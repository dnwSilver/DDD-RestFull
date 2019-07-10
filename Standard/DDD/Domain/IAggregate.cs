using Standard.RestFull;

namespace Standard.DDD
{
    public interface IAggregate
    {
        void SetEntity(Entity e);
        void SetValueObject(ValueObject e);

        IResponse MapToResponse();
    }
}