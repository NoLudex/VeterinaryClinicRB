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
            doc.Load("./././database/accessKey.xml");

            // Получаем список всех ключей доступа
            XmlNodeList ?keyNodes = doc.SelectNodes("/database/accessKeys/accessKey");

            // Проверяем, есть ли данный ключ доступа в БД
            if (keyNodes != null)
                foreach (XmlNode node in keyNodes)
                    if (node.InnerText == key)
                        return true;
            return false;
        }
        public static bool Update(string oldKey, string newKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("./././database/accessKey.xml");
            XmlNode ?oldKeyNode = doc.SelectSingleNode($"//accessKey[text()='{oldKey}']");

            if (oldKeyNode == null)
                return false;
            else
            {
                XmlNode ?newKeyNode = doc.SelectSingleNode($"//accessKey[text()='{newKey}']");
                if (newKeyNode != null)
                    return false;
                
                oldKeyNode.InnerText = newKey;
                doc.Save("./././database/accessKey.xml");

                return true;
            }
        }
        public static bool CheckAccess(string key)
        {
            string[] keys = new string[] 
            {
                "761QACA2MP", "WEMF8S5S5H", "SGE9X9YH67", "GB4L4P4YYA", "LADVNK26ES", "B412JP098F", 
                "8MYNBF1AR9", "N9X9GNYLSQ", "JSJBC55ZNL", "F3LPLQSJW8", "KC0OURJXS9", "Z9ZZEWZ49W",
                "EDD3O93CI3", "UX8FY5S5H5", "93QX9ZPG4W", "0H4P4YYANA", "IJHL0KSUMF", "7LBF1AJKDR",
                "VXMFEWCJEJ", "7V2SW27ETO", "W2I96910AK", "JI3CI3Z9Y6", "DWM6LFSTT6", "48N6UJKJ6V",
                "OHCAHERDAU", "I1IWV57XJ4", "R8GYM4Q4LL", "OLEGTOP4IK", "ILKVMOM228", "RBWEEXLOVE"
            };
            
            return keys.Contains(key);
        }
    }
}