using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Standard.Mapping
{
    /// <summary>
    ///     Класс для преобразования условий и замене полей доменной сущности на поля из модели данных.
    /// </summary>
    /// <typeparam name="TSource">Первоисточник выражения.</typeparam>
    /// <typeparam name="TDestination">Класс для которого будет перестроено выражение.</typeparam>
    /// <typeparam name="TPropertyMapper">Класс для сопоставляющий поля <see cref="TSource" /> и <see cref="TDestination" />.</typeparam>
    public class ExpressionRewrite<TSource, TDestination, TPropertyMapper> : ExpressionVisitor
        where TPropertyMapper : IPropertyMapper, new()
    {
        private readonly Stack<ParameterExpression[]> _lambdaStack = new Stack<ParameterExpression[]>();

        private readonly TPropertyMapper _mapper = new TPropertyMapper();

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
                propertyName = this._mapper.GetPropertyModelName(propertyName);

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
