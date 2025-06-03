namespace SHSSDMS_Interfaces
{
    public interface IMessagePlugin : IPlugin
    {
        void WriteMessage(string message);
    }
}
