using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace VeterinaryClinicRB
{
    partial class __main__
    {
        // Здесь хранятся все методы приложения
        // Найти по ID
        static XmlNode? FindDoctorById(XmlDocument doc, int ID)
        {
            XmlNode ?doctorsNodes = doc.SelectSingleNode("/doctors/doctor[name='" + ID + "']");
            if (doctorsNodes != null)
                return doctorsNodes;
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