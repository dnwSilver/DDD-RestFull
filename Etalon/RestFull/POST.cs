using Standard.DDD;
using Standard.RestFull;
using Standard.SDK;

namespace Standard
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
            Entity entity = factoryEntity.CreateEntity(builders[0]);

            Factory factoryValueObject = null;
            ValueObject valueObject = factoryValueObject.CreateValueObject(builders[1]);

            IUnitOfWork unitOfWork = null;
            Result resultSave1;
            Result resultSave2;

            using (unitOfWork)
            {
                Repository entityRepository = null;
                resultSave1 = entityRepository.CreateEntity(entity);

                Repository entityValueObject = null;
                resultSave2 = entityValueObject.CreateValueObject(valueObject);
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
        public Result CreateEntity(Entity entity)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = entity.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Create(dataTransferObjectIn);
            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }

        // Infrastracture
        public Result CreateValueObject(ValueObject valueObject)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = valueObject.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Create(dataTransferObjectIn);
            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }
    }
}
