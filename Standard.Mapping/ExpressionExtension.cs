using System;
using System.Linq.Expressions;

namespace Standard.Mapping
{
    public static class ExpressionExtension
    {
        public static Expression<Func<TDestination, bool>> Map<TSource, TDestination, TMapper>(
            this Expression<Func<TSource, bool>> accountModelQuery)
            where TMapper : EntityMapper, new()
        {
            var accountQuery =
                (Expression<Func<TDestination, bool>>) new ExpressionRewrite<TDestination, TSource, TMapper>().Visit(
                    accountModelQuery);

            return accountQuery;
        }
    }
}
