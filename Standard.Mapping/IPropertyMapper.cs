namespace Standard.Mapping
{
    /// <summary>
    ///     Класс для сопоставление полей объектов.
    /// </summary>
    public interface IPropertyMapper
    {
        /// <summary>
        /// Метод для определения наименования конечного свойства.
        /// </summary>
        /// <param name="sourcePropertyName">Начальное наименование свойства.</param>
        /// <returns>Конечное наименование свойства.</returns>
        string GetPropertyModelName(string sourcePropertyName);
    }
}
