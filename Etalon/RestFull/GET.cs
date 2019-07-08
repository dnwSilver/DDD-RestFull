using Standard.DDD;
using Standard.RestFull;
using Standard.SDK;

namespace Standard
{
    partial class API
    {
        // Presentation
        IResponse Get(IRequest request)
        {
            // Presentation
            ISpecification[] specs = request.MapToSpecs();
            Service someService = null;
            IAggregate aggregate = someService.Search(specs);
            return aggregate.MapToResponse();
        }

        // Presentation
        IResponse Head(IRequest request)
        {
            // Presentation
            ISpecification[] specs = request.MapToSpecs();
            Service someService = null;
            IAggregate aggregate = someService.Search(specs);
            return new Result().MapToResponse();
        }
    }

    partial class Service
    {
        // Domain
        public IAggregate Search(ISpecification[] specs)
        {
            Repository repositoryEntity = null;
            Entity entity = repositoryEntity.ReadEntity(specs);

            Repository repositoryValueObject = null;
            ValueObject valueObject = repositoryValueObject.ReadValueObject(specs);

            IAggregate aggregate = null;
            aggregate.SetEntity(entity);
            aggregate.SetValueObject(valueObject);

            IIntegrationEvent integrationEvent = null;
            IBus bus = null;
            bus.SendEvent(integrationEvent);

            return aggregate;
        }
    }

    partial class Repository
    {
        // Infrastracture
        public Entity ReadEntity(ISpecification[] specs)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = specs[0].MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Search(dataTransferObjectIn);

            IBuilder builderEntity = dataTransferObjectOut.MapToBuilder();
            Factory factoryEntity = null;
            Entity entity = factoryEntity.RecoveryEntity(builderEntity);

            return entity;
        }

        // Infrastracture
        public ValueObject ReadValueObject(ISpecification[] specs)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = specs[0].MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Search(dataTransferObjectIn);

            IBuilder builderValueObject = dataTransferObjectOut.MapToBuilder();
            Factory factoryValueObject = null;
            ValueObject valueObject = factoryValueObject.RecoveryValueObject(builderValueObject);

            return valueObject;
        }
    }
}
