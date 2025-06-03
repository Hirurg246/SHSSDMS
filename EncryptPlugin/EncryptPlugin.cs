using SHSSDMS_Interfaces;
using System.Text;
using System.Text.Json;

namespace EncryptPlugin
{
    public class EncryptPlugin : IEncryptPlugin
    {
        protected Dictionary<string, (string SecretWord, string SecretKey)>? keys = null;
        protected Random rand = new Random();
        public string Name => "Сквозное шифрование";
        public string Version => "1.0";

        public EncryptPlugin() { }

        public string? Decrypt(string data, string options, IModuleCore core)
        {
            if (data is null || data.Length < 65)
            {
                core.WriteMessage($"Неверный формат сообщения.");
                return null;
            }
            if (keys is null && !GetKeys(core)) return null;
            if (!keys.ContainsKey(options))
            {
                core.WriteMessage($"Ключ \"{options}\" не найден.");
                return null;
            }
            var key = keys[options];
            int offset = data[0];
            byte[] bData = Encoding.UTF8.GetBytes(data);
            for (int i = 1; i < bData.Length; i++)
            {
                for (int j = offset; j < key.SecretKey.Length && i < bData.Length; j++, i++)
                {
                    bData[i] -= (byte)key.SecretKey[j];
                }
            }
            string secWord = Encoding.UTF8.GetString(bData, 1, 64);
            if (secWord != key.SecretWord)
            {
                core.WriteMessage($"Ошибка в проверочном слове.");
                return null;
            }
            return Encoding.UTF8.GetString(bData, 65, data.Length - 65);
        }

        public string? Encrypt(string data, string options, IModuleCore core)
        {
            if (data is null)
            {
                core.WriteMessage($"Неверный формат сообщения.");
                return null;
            }
            if (keys is null && !GetKeys(core)) return null;
            if (!keys.ContainsKey(options))
            {
                core.WriteMessage($"Ключ \"{options}\" не найден.");
                return null;
            }
            var key = keys[options];
            int offset = rand.Next(0, key.SecretKey.Length / 2);
            data = offset + key.SecretWord + data;
            byte[] bData = Encoding.UTF8.GetBytes(data);
            for (int i = 1; i < bData.Length; i++)
            {
                for (int j = offset; j < key.SecretKey.Length && i < bData.Length; j++, i++)
                {
                    bData[i] += (byte)key.SecretKey[j];
                }
            }
            return Encoding.UTF8.GetString(bData);
        }

        protected bool GetKeys(IModuleCore core)
        {
            string? data = core.ReadFile(@"Plugins\Encryption\keys.json");
            if (data is null) return false;
            try
            {
                JsonSerializerOptions opt = new() { IncludeFields = true };
                keys = JsonSerializer.Deserialize<Dictionary<string, (string SecretWord, string SecretKey)>>(data, opt);
                return true;
            }
            catch (Exception ex)
            {
                core.WriteMessage($"Не удалось расшифровать файл ключей. ({ex.Message})");
                return false;
            }
        }
    }
}
