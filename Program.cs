using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace VeterinaryClinicRB
{
    class Program
    {
        public static string? key = "";
        public static int j = 5;
        static void Main(string[] args)
        {
            // Получение ключ из app.config
            // string pas = "OLEGTOP4IKdadada";
            // Console.WriteLine(Encrypt.Get(pas, 3));
            // Console.WriteLine(Decrypt.Get("ROHJWRS4LNgdgdgd==", 3));
            // Title.Wait();

            // key = ConfigurationManager.AppSettings["AccessKey"];
            // if (ConfigurationManager.AppSettings["AutoKey"].ToLower() != "true")
            // {
            //     key = "";
            //     Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //     config.AppSettings.Settings["AccessKey"].Value = "";
            //     config.Save(ConfigurationSaveMode.Modified);
            // }

            // Console.BackgroundColor = ConsoleColor.Green;
            // Console.ForegroundColor = ConsoleColor.Yellow;  
            
            // Первая проверка ключа
            if (key == "")
            {
                AccessKey.InputKey(key);
                key = Console.ReadLine();
            }
            while (true)
            {
                // Проверка ключа доступа на соответстиве ключам
                if (AccessKey.InputKey(key))
                {
                    General.Menu();
                }
                else
                {
                    AccessKey.Error(j);
                    j += 5;
                    Console.Clear();
                    Title.Set("Верификация");
                    Console.Write("Чтобы иметь доступ к программе, нужен специальный ключ. \nВы можете обратиться к администрации для получения данного ключа.\nВвод: ");
                    key = Console.ReadLine();
                }
            }
        }
    }
}