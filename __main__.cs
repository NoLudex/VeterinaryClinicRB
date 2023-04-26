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

            XmlNodeList ?xmlNodeList = doc.DocumentElement?.SelectNodes("./doctors/doctor");
            // Поиск врача по ID
            XmlNodeList ?doctorNode = FindDoctorsByName(doc, "Оktl");
            if (doctorNode != null)
            {   
                if (xmlNodeList != null) 
                {
                    foreach (XmlNode xmlNode in xmlNodeList)
                    {
                        string ?id = xmlNode.SelectSingleNode("id")?.InnerText;
                        string ?name = xmlNode.SelectSingleNode("name")?.InnerText;
                        string ?birthday = xmlNode.SelectSingleNode("birthday")?.InnerText;
                        string ?experience = xmlNode.SelectSingleNode("experience")?.InnerText;
                        string ?animalsTreated = xmlNode.SelectSingleNode("animals-treated")?.InnerText;
                        string ?telegramId = xmlNode.SelectSingleNode("telegram-id")?.InnerText;
                        
                        if (id != null && name != null && birthday != null && experience != null && animalsTreated != null && telegramId != null)
                        {
                            doctors.Add(new Doctors(id, name, birthday, experience, animalsTreated, telegramId));
                        }
                        else
                        {
                            Console.WriteLine("Произошла внутренняя ошибка с вычислением данных.");
                        }
                    }
                    foreach (Doctors doctor in doctors)
                    {
                        Console.WriteLine("{0,-9}{1,-21}{2,-12}{3,-12}{4,-16}{5,-14}", doctor.id, doctor.name, doctor.birthday, doctor.experience, doctor.animalsTreated, doctor.telegramId);
                    }
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
            else
            {
                Console.WriteLine("Доктор не найден");
            }
            // // Обновить данные врача по ID
            // UpdateDoctorById(doc, 2, "Иванова Иванна Ивановна", "02.02.1975", 12, 150, "@ivanova_ivan");

            // // Поиск врачей по имени
            // XmlNodeList ?doctorsNodes = FindDoctorsByName(doc, "Иванова Иванна Ивановна");
            // if (doctorsNodes != null)
            // {
            //     foreach (XmlNode node in doctorsNodes)
            //     {
            //         Console.WriteLine(node.OuterXml);
            //     }
            // }

            // // Сохранить изменения в файл
            // doc.Save(filePath);
        }
    }
}