using Etalon.DDD;

namespace Etalon
{
    partial class API
    {
        // Presentation
        // Idempotent
        IResponse Post(IRequest request)
        {
            // Presentation
            IBuilder[] builders = request.MapToBuilders();
            Service someService = null;
            Result result = someService.Create(builders);
            return result.MapToResponse();
        }
    }

    partial class Service
    {
        // Domain
        public Result Create(IBuilder[] builders)
        {
            Factory factoryEntity = null;
            IEntity entity = factoryEntity.CreateEntity(builders[0]);

            Factory factoryValueObject = null;
            IValueObject valueObject = factoryValueObject.CreateValueObject(builders[1]);

            IUnitOfWork unitOfWork = null;
            Result resultSave1;
            Result resultSave2;

            using (unitOfWork)
            {
                Repository entityRepository = null;
                resultSave1 = entityRepository.SaveEntity(entity);

                Repository entityValueObject = null;
                resultSave2 = entityValueObject.SaveValueObject(valueObject);
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
        public Result SaveEntity(IEntity entity)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = entity.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Create(dataTransferObjectIn);
            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }

        // Infrastracture
        public Result SaveValueObject(IValueObject valueObject)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = valueObject.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Create(dataTransferObjectIn);
            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }
    }
}
