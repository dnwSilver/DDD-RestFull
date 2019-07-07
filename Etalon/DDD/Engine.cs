using System;

namespace Etalon.DDD
{
    public class Factory
    {
        public IEntity CreateEntity(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }


        public IEntity RecoveryEntity(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }

        public IValueObject CreateValueObject(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }
        public IValueObject RecoveryValueObject(IBuilder b)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return null;
        }

        internal Result DisposalPermit(IEntity entity)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return new Result();
        }

        internal Result DisposalPermit(IValueObject entity)
        {
            ISpecification spec = null;
            spec.IsSatisfiedBy();
            return new Result();
        }
    }

    public interface ISpecification
    {
        bool IsSatisfiedBy();
        IDataTransferObjectIn MapToDTOIn();
    }

    public interface IUnitOfWork : IDisposable
    {
        void Create();
    }
    public interface IIntegrationEvent
    {

    }

    public interface IBus
    {
        void SendEvent(IIntegrationEvent @event);
    }

    public interface IRequest
    {
        IBuilder[] MapToBuilders();
        IBuilder MapToBuilder();
        ISpecification[] MapToSpecs();
        Command MapToCommand();
    }

    public interface IDataTransferObjectIn { }
    public interface IDataTransferObjectOut
    {
        IBuilder MapToBuilder();
        Result MapToResult();
    }
    public interface IDataSource
    {
        IDataTransferObjectOut Search(IDataTransferObjectIn input);
        IDataTransferObjectOut Create(IDataTransferObjectIn dataTransferObjectIn);
        IDataTransferObjectOut Delete(IDataTransferObjectIn dataTransferObjectIn);
    }

    public interface IBuilder
    {
        int SomeField();
    }

    public enum Command
    {
        Register
    }
    public interface IEntity
    {
        IDataTransferObjectIn MapToDTOIn();
        Result SomeMethod(int v);
    }

    public interface IValueObject
    {
        IDataTransferObjectIn MapToDTOIn();
        Result SomeMethod(int v);
    }

    public interface IAggregate
    {
        void SetEntity(IEntity e);
        void SetValueObject(IValueObject e);

        IResponse MapToResponse();
    }

    public interface IResponse { }

    public struct Result
    {
        internal bool Success { get; }
        internal bool Failure { get; }
        internal static Result Combine(Result resultSave1, Result resultSave2)
        {
            throw new NotImplementedException();
        }

        internal IResponse MapToResponse()
        {
            throw new NotImplementedException();
        }
    }
}
