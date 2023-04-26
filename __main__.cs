using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace VeterinaryClinicRB
{
    class __main__
    {
        public static void Main(string[] args)
        {
            string filePath = "./database/doctors.xml";

            // Создаем документ и загружаем из файла
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // Поиск врача по ID
            XmlNode ?doctorNode = FindDoctorById(doc, 1);
            if (doctorNode != null)
            {
                Console.WriteLine(doctorNode.OuterXml);
            }
            else
            {
                Console.WriteLine("Доктор не найден");
            }
            
            // Обновить данные врача по ID
            UpdateDoctorById(doc, 2, "Иванова Иванна Ивановна", "02.02.1975", 12, 150, "@ivanova_ivan");

            // Поиск врачей по имени
            XmlNodeList ?doctorsNodes = FindDoctorsByName(doc, "Иванова Иванна Ивановна");
            if (doctorsNodes != null)
            {
                foreach (XmlNode node in doctorsNodes)
                {
                    Console.WriteLine(node.OuterXml);
                }
            }

            // Сохранить изменения в файл
            doc.Save(filePath);
        }

        // Найти по ID
        static XmlNode? FindDoctorById(XmlDocument doc, int ID)
        {
            XmlNode ?doctorNode = doc.SelectSingleNode("/doctors/doctor[id='" + ID + "']");
            if (doctorNode != null)
                return doctorNode;
            else
                return null;
        }
        
        // Найти по Имени
        static XmlNodeList? FindDoctorsByName(XmlDocument doc, string name)
        {
            XmlNodeList ?doctorsNodes = doc.SelectNodes("/doctors/doctor[name='" + name + "']");
            if (doctorsNodes != null)
                return doctorsNodes;
            else
                return null;
        }

        // Обновить данные по ID
        static void UpdateDoctorById(XmlDocument doc, int ID, string name, string birthday, int experience, int animalsTreated, string telegramId)
        {
            XmlNode ?doctorNode = FindDoctorById(doc, ID);
            if (doctorNode != null)
            {
                XmlNode ?nameNode = doctorNode.SelectSingleNode("name");
                if (nameNode != null)
                    nameNode.InnerText = name;
                else
                    Console.WriteLine("Ошибка в имени.");

                XmlNode ?birthdayNode = doctorNode.SelectSingleNode("birthday");
                if (birthdayNode != null)
                    birthdayNode.InnerText = birthday;
                else
                    Console.WriteLine("Ошибка в дате рождения.");

                XmlNode ?experienceNode = doctorNode.SelectSingleNode("experience");
                if (experienceNode != null)
                    experienceNode.InnerText = experience.ToString();
                else
                    Console.WriteLine("Ошибка в указании опыта работы.");

                XmlNode ?animalsTreatedNode = doctorNode.SelectSingleNode("animals-treated");
                if (animalsTreatedNode != null)
                    animalsTreatedNode.InnerText = animalsTreated.ToString();
                else
                    Console.WriteLine("Ошибка в кол-ве животных, прошедших лечение.");

                XmlNode ?telegramIdNode = doctorNode.SelectSingleNode("telegram-id");
                if (telegramIdNode != null)
                    telegramIdNode.InnerText = telegramId;
                else
                    Console.WriteLine("Ошибка в указании ID телеграмма.");
            }
        }
    }
}