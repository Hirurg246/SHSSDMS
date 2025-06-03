using SHSSDMS_Interfaces;

namespace MessagePlugin
{
    public class MessagePlugin : IMessagePlugin
    {
        public string Name => "Консольный вывод";

        public string Version => "1.0";

        public MessagePlugin() { }

        public void WriteMessage(string message) => Console.WriteLine(DateTime.Now.ToString() + ": " + message);
    }
}
