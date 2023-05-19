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
    public class Lang
    {
        private static readonly string lang = ConfigurationManager.AppSettings["Lang"];
        private static readonly JObject json = JObject.Parse(File.ReadAllText($"./lang/{lang.ToLower()}.json"));

        public static string GetText(string key, params object[] args)
        {
            CultureInfo culture = CultureInfo.CurrentCulture; 
            var text = (string)json.SelectToken(key) ?? $"[{key}]";
            if (args.Length > 0) text = string.Format(culture, text, args);
            return text;
        }
    }
}