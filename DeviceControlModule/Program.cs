namespace DeviceControlModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DeviceControlModuleCore authModuleCore = new DeviceControlModuleCore();
                while (true) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска модуля управления устройствами. ({ex.Message})");
            }
        }
    }
}
