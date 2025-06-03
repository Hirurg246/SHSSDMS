namespace SHSSDMS_Interfaces
{
    public interface INetworkPlugin : IPlugin
    {
        Task ReceiveConnections(IModuleCore core);
        string? SendMessage(string message, string address);
    }
}
