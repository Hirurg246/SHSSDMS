namespace AuthModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AuthModuleCore authModuleCore = new AuthModuleCore();
                while (true) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска модуля аутентификации/авторизации. ({ex.Message})");
            }
        }
    }
}
