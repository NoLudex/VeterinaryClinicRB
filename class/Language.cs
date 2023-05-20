using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Globalization;
using System.Configuration;

namespace VeterinaryClinicRB
{
    public  partial class Lang
    {
        private static readonly string lang = ConfigurationManager.AppSettings["Lang"];
        private static readonly JObject json = JObject.Parse(File.ReadAllText($"./lang/{lang.ToLower()}.json"));

        public static string GetText(string key, params object[] args)
        {
            CultureInfo culture = CultureInfo.CurrentCulture; 
            var text = (string)json.SelectToken(key) ?? $"[{key}]";
            if (args.Length > 0)
                text = string.Format(culture, text, args);
            if (text == "" || text == null)
                text = $"${key}~~";
            return text;
        }

        public static void Change(string LANG)
        {
            Console.Clear();
            Console.WriteLine("Ваш язык был успешно изменён на английский! (Требуется перезапуск программы!)");
            Config.Set("Lang", LANG); // На английской версии будет "изменён на русский"
            Title.Wait();
            Environment.Exit(0);
        }

        public static bool Check()
        {
            string language = Config.Get("Lang");
            if (language.ToLower() == "ru" || language.ToLower() == "en")
                return true;
            else
            {
                Console.Clear();
                Console.WriteLine("Error loading the program language. The language is set to English. (A restart of the program is required!)");
                Config.Set("Lang", "en");
                Console.WriteLine("[DEBUG] lang = " + language);
                Title.Wait();
                Environment.Exit(0);
                return false;
            }
        }
    }
}