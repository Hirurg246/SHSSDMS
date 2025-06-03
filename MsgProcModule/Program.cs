namespace MsgProcModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MsgProcModuleCore authModuleCore = new MsgProcModuleCore();
                while (true) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска модуля обработки сообщений. ({ex.Message})");
            }
        }
    }
}
