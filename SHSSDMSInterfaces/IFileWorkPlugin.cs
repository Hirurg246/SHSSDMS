namespace SHSSDMS_Interfaces
{
    public interface IFileWorkPlugin : IPlugin
    {
        string? ReadFile(string path, IModuleCore core);
        bool WriteFile(string path, string contents, bool append, IModuleCore core);
    }
}
