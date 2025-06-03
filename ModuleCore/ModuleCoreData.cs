using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCore
{
    public class ModuleCoreData
    {
        public int Port { get; set; }
        public string ControlModuleAdress { get; set; }
        public string AuthModuleAdress { get; set; }
        public string DataCollectModuleAdress { get; set; }
        public string DataStorageModuleAdress { get; set; }
        public string MsgProcModuleAdress { get; set; }
    }
}
