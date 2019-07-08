using Standard.SDK;

namespace Standard.DDD
{
    public class Factory
    {
        public Entity CreateEntity(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }


        public Entity RecoveryEntity(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }

        public ValueObject CreateValueObject(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }

        public ValueObject RecoveryValueObject(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }

        internal Result DisposalPermit(Entity entity)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return new Result();
        }

        internal Result DisposalPermit(ValueObject entity)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return new Result();
        }
    }
}