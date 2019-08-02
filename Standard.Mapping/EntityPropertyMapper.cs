using System;

namespace Standard.Mapping
{
    /// <summary>
    ///     Класс для сопоставление полей объекта <see cref="IEntityDomain"/>.
    /// </summary>
    public class EntityPropertyMapper : IPropertyMapper
    {
        /// <summary>
        /// Метод для определения наименования конечного свойства.
        /// </summary>
        /// <param name="sourcePropertyName">Начальное наименование свойства.</param>
        /// <returns>Конечное наименование свойства.</returns>
        public string GetPropertyModelName(string sourcePropertyName)
        {
            string destinationPropertyName;

            switch(sourcePropertyName)
            {
                case nameof(IEntityDomain.Bal):
                    destinationPropertyName = nameof(IEntityModel.Balance);

                    break;
                case nameof(IEntityDomain.Name):
                    destinationPropertyName = nameof(IEntityModel.CustomerName);

                    break;

                case nameof(IEntityDomain.Id):
                    destinationPropertyName = nameof(IEntityModel.Id);

                    break;

                default:
                    throw new Exception("Маппинг для классов настроен некорректно.");
            }

            return destinationPropertyName;
        }
    }
}
