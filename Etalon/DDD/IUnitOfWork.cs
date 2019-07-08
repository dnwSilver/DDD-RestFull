using System;

namespace Standard.DDD
{
    public interface IUnitOfWork : IDisposable
    {
        void Create();
    }
}