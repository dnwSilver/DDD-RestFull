using Standard.DDD;

namespace Standard.RestFull
{
    public interface IRequest
    {
        IBuilder[] MapToBuilders();
        IBuilder MapToBuilder();
        ISpecification[] MapToSpecs();
        Command MapToCommand();
    }
}