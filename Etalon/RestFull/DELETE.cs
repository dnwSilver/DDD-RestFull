using Etalon.DDD;

namespace Etalon
{
    partial class API
    {
        // Presentation
        // Idempotent
        IResponse Delete(IRequest request)
        {
            // Presentation
            ISpecification[] specs = request.MapToSpecs();
            Service someService = null;
            Result result = someService.Delete(specs);
            return result.MapToResponse();
        }
    }

    partial class Service
    {
        // Domain
        public Result Delete(ISpecification[] specs)
        {
            Repository repositoryEntity = null;
            IEntity entity = repositoryEntity.SearchEntity(specs);

            Repository repositoryValueObject = null;
            IValueObject valueObject = repositoryValueObject.SearchValueObject(specs);

            IUnitOfWork unitOfWork = null;
            Result resultSave1;
            Result resultSave2;

            using (unitOfWork)
            {
                Repository entityRepository = null;
                resultSave1 = entityRepository.DeleteEntity(entity);

                Repository entityValueObject = null;
                resultSave2 = entityValueObject.DeleteValueObject(valueObject);
            }

            IIntegrationEvent integrationEvent = null;
            IBus bus = null;
            bus.SendEvent(integrationEvent);

            return Result.Combine(resultSave1, resultSave2);
        }
    }

    partial class Repository
    {
        // Infrastracture
        public Result DeleteEntity(IEntity entity)
        {
            Factory factoryEntity = null;
            Result disposalPermitEntity = factoryEntity.DisposalPermit(entity);

            if (disposalPermitEntity.Failure)
                return new Result();

            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = entity.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Delete(dataTransferObjectIn);

            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }

        // Infrastracture
        public Result DeleteValueObject(IValueObject valueObject)
        {
            Factory factoryValueObject = null;
            Result disposalPermitValueObject = factoryValueObject.DisposalPermit(valueObject);

            if (disposalPermitValueObject.Failure)
                return new Result();

            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = valueObject.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Delete(dataTransferObjectIn);
            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }
    }
}
