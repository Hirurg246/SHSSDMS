using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MsgProcModule
{
    internal class MsgProcModuleCore : ModuleCore.ModuleCore
    {

        public override async Task ProcessConnection(TcpClient connection)
        {
            try
            {
                connection.SendTimeout = 5000;
                connection.ReceiveTimeout = 10000;
                NetworkStream stream = connection.GetStream();
                byte[] data = new byte[1024];
                int mode = stream.ReadByte();
                if (mode == 2) return;
                IPEndPoint IP = (IPEndPoint)connection.Client.RemoteEndPoint;
                string adress = IP.Address + ":" + IP.Port;
                stream.Read(data);
                string? msg = Encoding.UTF8.GetString(data);
                if (mode == 2) msg = Decrypt(msg, adress);
                string answer = MsgProcHelper.ProcessCommand(msg);
                if (mode == 2) answer = Encrypt(answer, adress);
                networkPlugin.SendMessage(answer, adress);
            }
            finally
            {
                connection.Close();
            }
        }

        public MsgProcModuleCore() : base()
        {
            _ = Task.Run(() => networkPlugin.ReceiveConnections(this));
        }

    }
}
