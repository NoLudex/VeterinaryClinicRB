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
using Newtonsoft.Json.Linq;
using System.IO;

namespace VeterinaryClinicRB
{
    class Program
    {
        public static bool authorizedKey = false;
        public static bool authorizedLogin = false;
        static void Main(string[] args)
        {            
            // Если программа запущена первый раз, то диалог с выбором языка
            Lang.Welcome();

            // Проверка языка, который активен, если он существует, то программа запустит меню со входом
            while (Lang.Check())
            {
                // Проверка ключа доступа на соответстиве ключам
                if (AccessKey.Check(Config.Get("AutoKey", true), authorizedKey))
                {
                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            if (authorizedKey == false)
                                authorizedKey = true;
                            Title.Theme(Title.activeTheme);
                            Authorization.Menu();
                        }
                        catch (System.Exception)
                        {
                            Console.Clear();
                            Console.WriteLine(
                                $"{Lang.GetText("extra_error_line_0")}\n" +
                                $"{Lang.GetText("extra_error_line_1")}\n" +
                                $"{Lang.GetText("extra_error_line_2")}"
                            );
                            Title.Wait();
                        }
                    }
                }
            }  
        }
    }
}