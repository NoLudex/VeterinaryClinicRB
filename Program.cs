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
        public static bool authorizedKey = false;
        public static bool authorizedLogin = false;
        static void Main(string[] args)
        {
            // Получение ключ из app.config
            // string pas = "OLEGTOP4IKdadada";
            // Console.WriteLine(Encrypt.Get(pas, 3));
            // Console.WriteLine(Decrypt.Get("ROHJWRS4LNgdgdgd==", 3));
            // Title.Wait();

            while (true)
            {
                // Проверка ключа доступа на соответстиве ключам
                if (AccessKey.Check(Config.Get("AutoKey", true), authorizedKey))
                {
                    if (authorizedKey == false)
                        authorizedKey = true;
                    Title.Theme(Convert.ToInt32(Config.Get("ConsoleTheme")));
                    General.Menu();
                }
                // else
                // {
                //     AccessKey.Error(j);
                //     j += 5;
                //     Console.Clear();
                //     Title.Set("Верификация");
                //     Console.Write("Чтобы иметь доступ к программе, нужен специальный ключ. \nВы можете обратиться к администрации для получения данного ключа.\nВвод: ");
                //     key = Console.ReadLine();
                // }
            }
        }
    }
}