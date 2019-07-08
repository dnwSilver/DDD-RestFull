namespace Standard.DDD
{
    public interface ISpecification
    {
        bool IsSatisfiedBy();
        IDataTransferObjectIn MapToDTOIn();
    }
}