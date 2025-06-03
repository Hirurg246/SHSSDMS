namespace DataStorageModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataStorageModuleCore authModuleCore = new DataStorageModuleCore();
                while (true) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска модуля контроля доступа к данным. ({ex.Message})");
            }
        }
    }
}
