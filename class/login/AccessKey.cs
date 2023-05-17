using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;

namespace VeterinaryClinicRB
{
    public partial class AccessKey
    {
        // Проверка ключа на валидность из массива ключей
        public static bool CheckAccess(string key)
        {
            // Ключи в кавычках
            string[] keys = new string[] 
            {
                "761QACA2MP", "WEMF8S5S5H", "SGE9X9YH67", "GB4L4P4YYA", "LADVNK26ES", "B412JP098F", 
                "8MYNBF1AR9", "N9X9GNYLSQ", "JSJBC55ZNL", "F3LPLQSJW8", "KC0OURJXS9", "Z9ZZEWZ49W",
                "EDD3O93CI3", "UX8FY5S5H5", "93QX9ZPG4W", "0H4P4YYANA", "IJHL0KSUMF", "7LBF1AJKDR",
                "VXMFEWCJEJ", "7V2SW27ETO", "W2I96910AK", "JI3CI3Z9Y6", "DWM6LFSTT6", "48N6UJKJ6V",
                "OHCAHERDAU", "I1IWV57XJ4", "R8GYM4Q4LL", "OLEGTOP4IK", "ILKVMOM228", "RBWEEXLOVE"
            };
            
            // Возвращает, есть ли ключ (true || false)
            return keys.Contains(key);
        }

        // Переключатель автоматического ввода ключа при входе
        public static void AutoCheckKey()
        {
            bool nowSettings = Config.Get("AutoKey", true);
            if (nowSettings)
            {
                Config.Set("AutoKey", false);
                Console.Clear();
                Console.WriteLine("Теперь при входе в приложение, нужно будет вводить ключ на регулярной основе");
            }
            else
            {
                Config.Set("AutoKey", true);
                Console.Clear();
                Console.WriteLine("Теперь при входе в приложение, не нужно вводить ключ повторно");
            }
            Title.Wait();
            return;
        }

        // Удалить сохранённый ключ
        public static void Delete()
        {
            Config.Set("AutoKey", false);
            Config.Set("AccessKey", "");
            Console.Clear();
            Console.WriteLine("Ключ доступа для данной сессии удалён, вам потребуется ввести его снова. (Приложение закроется)");
            Title.Wait();
            Environment.Exit(0);
        }

        // Посмотреть текущий ключ
        public static void Get()
        {
            string result = Decrypt.Get(Config.Get("AccessKey"), 2);
            Console.Clear();
            if (result == "Данный ключ отсутствует")
                Console.WriteLine("Программа была недавно запущена или ключ был удалён, перезапустите программу, чтобы получить ключ");
            else
                Console.WriteLine($"На данный момент авторизован ключ: {result}");
            Title.Wait();
        }
    }
}