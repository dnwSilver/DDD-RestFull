using System;
using System.Linq.Expressions;

namespace Standard.Mapping
{
    /// <summary>
    ///     Набор расширений для выражений.
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        ///     Метод конвертирующий выражения подменяю свойства одного объекта на свойства другого.
        /// </summary>
        /// <param name="mutableExpression">Изменяемое выражение.</param>
        /// <typeparam name="TSource">Изначальный объект используемый в выражении.</typeparam>
        /// <typeparam name="TDestination">Конечный объект используемый в выражении.</typeparam>
        /// <typeparam name="TPropertyMapper">Класс для сопоставление полей объектов.</typeparam>
        /// <returns>Изменённое выражение.</returns>
        public static Expression<Func<TDestination, bool>> Map<TSource, TDestination, TPropertyMapper>(
            this Expression<Func<TSource, bool>> mutableExpression)
            where TPropertyMapper : IPropertyMapper, new()
        {
            var modifiedExpression =
                new ExpressionRewrite<TDestination, TSource, TPropertyMapper>().Visit(mutableExpression);

            return (Expression<Func<TDestination, bool>>) modifiedExpression;
        }
    }
}
