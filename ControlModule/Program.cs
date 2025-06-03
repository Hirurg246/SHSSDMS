namespace ControlModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ControlModuleCore authModuleCore = new ControlModuleCore();
                while (true) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска оркестратора. ({ex.Message})");
            }
        }
    }
}
