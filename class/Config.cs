using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace VeterinaryClinicRB
{
    // Данный класс предназначен для работы с настройками приложения
    public class Config
    {
        // Установка значения
        public static void Set(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
        }

        // Установка значения формата bool
        public static void Set(string key, bool value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (value)
                config.AppSettings.Settings[key].Value = "True";
            else
                config.AppSettings.Settings[key].Value = "False";
            config.Save(ConfigurationSaveMode.Modified);
        }

        // Получение значения
        public static string Get(string key)
        {
            string? value = ConfigurationManager.AppSettings.Get(key);
            if (value == null || value == "")
                return "Данный ключ отсутствует  ";
            else
                return value;
        }

        // Получение значений формата bool
        public static bool Get(string key, bool isBool)
        {
            string value = Get(key);
            if (value.ToLower() == "true")
                return true;
            else
                return false;
        }
    }
}