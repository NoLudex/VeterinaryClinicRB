using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;

namespace VeterinaryClinicRB
{
    public class AccessKey
    {
        public static bool Validate()
        {
            string ?key = ConfigurationManager.AppSettings["AccessKey"];
            return !string.IsNullOrEmpty(key) && Validate(key);
        }

        public static bool Validate(string key)
        {
            // Проверяем длину ключа
            if (key.Length != 10)
                return false;

            // Проверяем, что ключ содержит только символы A-Z и 0-9
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            if (!regex.IsMatch(key))
                return false;

            // Загружаем конфигурацию из файла XML
            XmlDocument doc = new XmlDocument();
            doc.Load("././database/accessKey.xml");

            // Получаем список всех ключей доступа
            XmlNodeList ?keyNodes = doc.SelectNodes("/database/accessKeys/accessKey");

            // Проверяем, есть ли данный ключ доступа в БД
            if (keyNodes != null)
                foreach (XmlNode node in keyNodes)
                    if (node.InnerText == key)
                        return true;
            return false;
        }
    }
}