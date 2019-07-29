using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Standard.Mapping
{
    public class ExpressionRewrite<TSource, TDestination, TMapper> : ExpressionVisitor
        where TMapper : EntityMapper, new()
    {
        private readonly Stack<ParameterExpression[]> _lambdaStack = new Stack<ParameterExpression[]>();

        private readonly TMapper _mapper = new TMapper();

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var lambda = (LambdaExpression) node;

            this._lambdaStack.Push(lambda.Parameters.Select(parameter =>
                                                                typeof(TDestination) == parameter.Type
                                                                    ? Expression.Parameter(typeof(TSource))
                                                                    : parameter)
                                         .ToArray());

            lambda = Expression.Lambda(this.Visit(lambda.Body), this._lambdaStack.Pop());

            return lambda;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var memberExpression = node;
            var declaringType    = memberExpression.Member.DeclaringType;
            var propertyName     = memberExpression.Member.Name;

            if(typeof(TDestination) == declaringType)
            {
                propertyName = this._mapper.GetPropertyName(propertyName);

                memberExpression = Expression.Property(this.Visit(memberExpression.Expression),
                                                       typeof(TSource).GetProperty(propertyName));
            }

            return memberExpression;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            node = (ParameterExpression) base.VisitParameter(node);
            if(typeof(TDestination) == node.Type)
                node = this._lambdaStack.Peek().Single(parameter => parameter.Type == typeof(TSource));

            return node;
        }
    }
}
