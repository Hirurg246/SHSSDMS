using SHSSDMS_Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetworkPlugin
{
    public class NetworkPlugin : INetworkPlugin
    {
        public string Name => "Связь TCP";

        public string Version => "1.0";

        public NetworkPlugin() { }

        public async Task ReceiveConnections(IModuleCore core)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress IP = hostEntry.AddressList[0];
            foreach (IPAddress address in hostEntry.AddressList)
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP = address;
                    break;
                }
            TcpListener listener = new TcpListener(IP, core.Port);
            try
            {
                core.WriteMessage($"Сервер запущен на {listener.LocalEndpoint.ToString()}.");
                listener.Start();
                while (true)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => core.ProcessConnection(tcpClient));
                }
            }
            finally
            {
                listener.Stop();
            }
        }

        public string? SendMessage(string message, string address)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.SendTimeout = 5000;
                client.ReceiveTimeout = 10000;
                IPEndPoint dest = IPEndPoint.Parse(address);
                client.Connect(dest);
                byte[] data = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data);
                byte[] response = new byte[1024];
                stream.Read(response);
                client.Close();
                return Encoding.UTF8.GetString(response);
            }
            catch
            {
                return null;
            }
        }
    }
}
