using System.Net.Sockets;

namespace SHSSDMS_Interfaces
{
    public interface IModuleCore
    {
        int Port { get; }
        string? ReadFile(string path);
        bool WriteFile(string path, string contents, bool append);
        void WriteMessage(string message);
        Task ProcessConnection(TcpClient connection);
    }
}
