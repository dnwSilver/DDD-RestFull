namespace Standard.DDD
{

    public interface IBus
    {
        void SendEvent(IIntegrationEvent @event);
    }
}