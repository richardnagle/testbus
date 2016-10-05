namespace TestBus.Commands
{
    public interface IPingReply
    {
        string ClientIPAddress { get; }
        string ServerIPAddress { get; }
    }
}