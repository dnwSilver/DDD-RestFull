using System.Windows.Input;
using Standard.DDD;
using Standard.RestFull;
using Standard.SDK;

namespace Standard
{
    partial class API
    {
        // Presentation
        // Idempotent
        IResponse Patch(IRequest request)
        {
            // Presentation
            ISpecification[] specs = request.MapToSpecs();
            IBuilder builder = request.MapToBuilder();
            Command command = request.MapToCommand();
            Service someService = null;

            Result result;
            switch (command)
            {
                case Command.Register:
                    result = someService.SomeMethod(specs, builder);
                    break;
                default:
                    result = new Result();
                    break;
            }

            return result.MapToResponse();
        }
    }

    partial class Service
    {
        // Domain
        public Result SomeMethod(ISpecification[] specs, IBuilder builder)
        {
            Repository repositoryEntity = null;
            Entity entity = repositoryEntity.ReadEntity(specs);

            Repository repositoryValueObject = null;
            ValueObject valueObject = repositoryValueObject.ReadValueObject(specs);

            Result resultMethod1 = entity.SomeMethod(builder.SomeField());
            Result resultMethod2 = entity.SomeMethod(builder.SomeField());
            valueObject.SomeMethod(builder.SomeField());

            if (Result.Combine(resultMethod1, resultMethod2).Failure)
                return Result.Combine(resultMethod1, resultMethod2);

            IUnitOfWork unitOfWork = null;
            Result resultSave1;
            Result resultSave2;

            using (unitOfWork)
            {
                Repository entityRepository = null;
                resultSave1 = entityRepository.UpdateEntity(entity);

                Repository entityValueObject = null;
                resultSave2 = entityValueObject.UpdateValueObject(valueObject);
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
        public Result UpdateEntity(Entity entity)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = entity.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Create(dataTransferObjectIn);
            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }

        // Infrastracture
        public Result UpdateValueObject(ValueObject valueObject)
        {
            IDataSource dataSource = null;
            IDataTransferObjectIn dataTransferObjectIn = valueObject.MapToDTOIn();
            IDataTransferObjectOut dataTransferObjectOut = dataSource.Create(dataTransferObjectIn);
            Result result = dataTransferObjectOut.MapToResult();
            return result;
        }
    }
}
