using System;

namespace Standard.Mapping
{
    public interface IMapper<TDomain, TModel>
    {
        TDomain Map(TModel model);

        TModel Map(TDomain domain);

        string GetPropertyName(string sourcePropertyName);
    }

    public class EntityMapper<TSource, TDestination>
    {
        public string GetPropertyName(string sourcePropertyName)
        {
            string destinationPropertyName;

            switch(sourcePropertyName)
            {
                case nameof(EntityDomain.Bal):
                    destinationPropertyName = nameof(EntityModel.Balance);

                    break;
                case nameof(EntityDomain.Name):
                    destinationPropertyName = nameof(EntityModel.CustomerName);

                    break;

                case nameof(EntityDomain.Id):
                    destinationPropertyName = nameof(EntityModel.Id);

                    break;

                default:
                    throw new Exception("Маппинг для классов настроен некорректно.");
            }

            return destinationPropertyName;
        }
    }
}
