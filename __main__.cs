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
        public static void Main(string[] args)
        {
            List<Doctors> doctors = new List<Doctors>();
            string filePath = "./database/doctors.xml";

            // Создаем документ и загружаем из файла
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // Поиск врача по ID
            XmlNode ?doctorNode = FindDoctorById(doc, 1);
            if (doctorNode != null)
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    doctors.Add(new Doctors
                    {
                        id = xmlNode.SelectSingleNode("id").InnerText,
                        name = xmlNode.SelectSingleNode("name").InnerText,
                        birthday = xmlNode.SelectSingleNode("birthday").InnerText,
                        experience = xmlNode.SelectSingleNode("experience").InnerText,
                        animalsTreated = xmlNode.SelectSingleNode("animals-treated").InnerText,
                        telegramId = xmlNode.SelectSingleNode("telegram-id").InnerText
                    });
                }
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
    }
}