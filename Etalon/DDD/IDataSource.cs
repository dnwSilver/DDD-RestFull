using Standard.SDK;

namespace Standard.DDD
{

    public interface IDataSource
    {
        IDataTransferObjectOut Search(IDataTransferObjectIn input);
        IDataTransferObjectOut Create(IDataTransferObjectIn dataTransferObjectIn);
        IDataTransferObjectOut Delete(IDataTransferObjectIn dataTransferObjectIn);
    }

    public interface IDataTransferObjectIn
    {
        
    }
    
    public interface IDataTransferObjectOut
    {
        IBuilder MapToBuilder();
        Result MapToResult();
    }
}