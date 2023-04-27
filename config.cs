using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Text.RegularExpressions;

namespace VeterinaryClinicRB
{
    partial class __main__
    {
        public static int GetMaxXMLId(string FileName)
        {
            switch (FileName)
            {
                case "doctor":
                string ?maxValue = ConfigurationManager.AppSettings["MaxDoctorId"];
                if (!int.TryParse (maxValue, out int result))
                    result = 0;
                return result;
                default:
                Console.WriteLine("|notFoundFile|");
                return 0;
            }
        }
        
        public static bool ValidateAccessKey()
        {
            string ?key = ConfigurationManager.AppSettings["AccessKey"];
            return !string.IsNullOrEmpty(key) && ValidateAccessKey(key);
        }

        public static bool ValidateAccessKey(string key)
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
            doc.Load("./database/accessKey.xml");

            // Получаем список всех ключей доступа
            XmlNodeList ?keyNodes = doc.SelectNodes("/database/accessKeys/accessKey");

            // Проверяем, есть ли данный ключ доступа в БД
            if (keyNodes != null)
                foreach (XmlNode node in keyNodes)
                    if (node.InnerText == key)
                        return true;
            return false;
        }
        public static bool UpdateAccessKey(string oldKey, string newKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("./database/accessKey.xml");
            XmlNode ?oldKeyNode = doc.SelectSingleNode($"//accessKey[text()='{oldKey}']");

            if (oldKeyNode == null)
                return false;
            else
            {
                XmlNode ?newKeyNode = doc.SelectSingleNode($"//accessKey[text()='{newKey}']");
                if (newKeyNode != null)
                    return false;
                
                oldKeyNode.InnerText = newKey;
                doc.Save("./database/accessKey.xml");

                return true;
            }
        }
    }
}