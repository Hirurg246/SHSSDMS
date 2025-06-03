using SHSSDMS_Interfaces;
using System.IO;

namespace FileWorkPlugin
{
    public class FileWorkPlugin : IFileWorkPlugin
    {
        public string Name => "Чтение файлов System.IO";

        public string Version => "1.0";

        public FileWorkPlugin() { }

        public string? ReadFile(string path, IModuleCore core)
        {
            if (!File.Exists(path))
            {
                core.WriteMessage($"Файл \"{path}\" не найден.");
                return null;
            }
            return File.ReadAllText(path);
        }

        public bool WriteFile(string path, string contents, bool append, IModuleCore core)
        {
            try
            {
                if (append) File.AppendAllText(path, contents);
                else File.WriteAllText(path, contents);
                return true;
            }
            catch(Exception ex)
            {
                core.WriteMessage($"Не удалось провести запись в файл \"{path}\". ({ex.Message})");
                return false;
            }
        }
    }
}
