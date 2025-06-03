namespace DataCollectModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataCollectModuleCore authModuleCore = new DataCollectModuleCore();
                while (true) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска модуля сбора данных. ({ex.Message})");
            }
        }
    }
}
