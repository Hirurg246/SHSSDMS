using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule
{
    internal class AuthModuleCore : ModuleCore.ModuleCore
    {
        public override Task ProcessConnection(TcpClient connection)
        {
            throw new NotImplementedException();
        }

        public AuthModuleCore() : base() { }
    }
}
