using McMaster.NETCore.Plugins;
using SHSSDMS_Interfaces;
using System.Net.Sockets;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ModuleCore
{
    public abstract class ModuleCore : IModuleCore
    {
        public int Port => moduleCoreData.Port;

        public abstract Task ProcessConnection(TcpClient connection);

        protected Action<string> writeMessage;

        protected Func<string, string, string?> encrypt;

        protected Func<string, string, string?> decrypt;

        public ModuleCoreData moduleCoreData;

        protected IFileWorkPlugin? fileWorkPlugin { get; } = null;

        protected INetworkPlugin? networkPlugin { get; } = null;

        protected IEncryptPlugin? encryptPlugin { get; } = null;

        protected IMessagePlugin? messagePlugin { get; } = null;

        public string? ReadFile(string path) => fileWorkPlugin.ReadFile(path, this);

        public bool WriteFile(string path, string contents, bool append) => fileWorkPlugin.WriteFile(path, contents, append, this);

        public void WriteMessage(string message) => writeMessage(message);

        protected string? Encrypt(string data, string options) => encrypt(data, options);

        protected string? Decrypt(string data, string options) => decrypt(data, options);

        protected ModuleCore()
        {
            try
            {
                PluginLoader loader = PluginLoader.CreateFromAssemblyFile(@"Plugins\FileWork\FileWorkPlugin.dll",
                    [typeof(IFileWorkPlugin)]);
                var plugin = loader.LoadDefaultAssembly().GetTypes().
                    Where(t => typeof(IFileWorkPlugin).IsAssignableFrom(t) && !t.IsAbstract).FirstOrDefault();
                fileWorkPlugin = (IFileWorkPlugin)Activator.CreateInstance(plugin);
                Console.WriteLine($"Плагин {fileWorkPlugin.Name} запущен.");
            }
            catch
            {
                Console.WriteLine("Ошибка загрузки плагина работы с файлами.");
                throw new DllNotFoundException("FileWorkPlugin.dll");
            }
            try
            {
                PluginLoader loader = PluginLoader.CreateFromAssemblyFile(@"Plugins\Network\NetworkPlugin.dll",
                    [typeof(INetworkPlugin)]);
                var plugin = loader.LoadDefaultAssembly().GetTypes().
                   Where(t => typeof(INetworkPlugin).IsAssignableFrom(t) && !t.IsAbstract).FirstOrDefault();
                networkPlugin = (INetworkPlugin)Activator.CreateInstance(plugin);
                Console.WriteLine($"Плагин {networkPlugin.Name} запущен.");
            }
            catch
            {
                Console.WriteLine("Ошибка загрузки сетевого плагина.");
                throw new DllNotFoundException("NetworkPlugin.dll");
            }
            try
            {
                PluginLoader loader = PluginLoader.CreateFromAssemblyFile(@"Plugins\Encryption\EncryptPlugin.dll",
                    [typeof(IEncryptPlugin)]);
                var plugin = loader.LoadDefaultAssembly().GetTypes().
                   Where(t => typeof(IEncryptPlugin).IsAssignableFrom(t) && !t.IsAbstract).FirstOrDefault();
                encryptPlugin = (IEncryptPlugin)Activator.CreateInstance(plugin);
                Console.WriteLine($"Плагин {encryptPlugin.Name} запущен.");
                encrypt = (a, b) => encryptPlugin.Encrypt(a, b, this);
                decrypt = (a, b) => encryptPlugin.Decrypt(a, b, this);
            }
            catch
            {
                Console.WriteLine("Ошибка загрузки шифровального плагина, шифрование не используется.");
                encrypt = (a, b) => a;
                decrypt = (a, b) => a;
            }
            try
            {
                PluginLoader loader = PluginLoader.CreateFromAssemblyFile(@"Plugins\Message\MessagePlugin.dll",
                    [typeof(IMessagePlugin)]);
                var plugin = loader.LoadDefaultAssembly().GetTypes().
                   Where(t => typeof(IMessagePlugin).IsAssignableFrom(t) && !t.IsAbstract).FirstOrDefault();
                messagePlugin = (IMessagePlugin)Activator.CreateInstance(plugin);
                Console.WriteLine($"Плагин {messagePlugin.Name} запущен.");
            }
            catch
            {
                Console.WriteLine("Ошибка загрузки плагина сообщений, используется вывод на консоль.");
                writeMessage = Console.WriteLine;
            }
            try
            {
                JsonSerializerOptions opt = new() { IncludeFields = true };
                moduleCoreData = JsonSerializer.Deserialize<ModuleCoreData>(fileWorkPlugin.ReadFile(@"bin\data.bin", this), opt);
                if (moduleCoreData is null) throw new FileNotFoundException("data.bin");
            }
            catch
            {
                throw new FileNotFoundException("data.bin");
            }
        }
    }
}
