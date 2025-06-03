namespace SHSSDMS_Interfaces
{
    public interface IEncryptPlugin : IPlugin
    {
        string? Encrypt(string data, string options, IModuleCore core);
        string? Decrypt(string data, string options, IModuleCore core);
    }
}
