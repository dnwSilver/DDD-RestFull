using System;
using System.Collections.Generic;
using System.Text;
using Standard.DDD;
using Standard.SDK;

namespace Standard.RestFull
{
    partial class API
    {
        // Presentation
        // Idempotent
        IResponse Put(IRequest request)
        {
            // Presentation
            IBuilder builder = request.MapToBuilder();
            Service someService = null;
            Result result = someService.Update(builder);

            return result.MapToResponse();
        }
    }

    partial class Service
    {
        public Result Update(IBuilder builder)
        {
            Factory factoryEntity = null;
            Entity newEntity = factoryEntity.CreateEntity(builder);

            Factory factoryValueObject = null;
            ValueObject newValueObject = factoryValueObject.CreateValueObject(builder);


            IUnitOfWork unitOfWork = null;
            Result resultSave1;
            Result resultSave2;

            using (unitOfWork)
            {
                Repository entityRepository = null;
                resultSave1 = entityRepository.UpdateEntity(newEntity);

                Repository entityValueObject = null;
                resultSave2 = entityValueObject.UpdateValueObject(newValueObject);
            }

            IIntegrationEvent integrationEvent = null;
            IBus bus = null;
            bus.SendEvent(integrationEvent);

            return Result.Combine(resultSave1, resultSave2);
        }
    }
}
