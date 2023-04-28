using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;

namespace VeterinaryClinicRB
{
    public class XmlRead
    {
        static void ShowByID(string FileName, string MainTag, string ObjectTag, int id)
        {
            Title.Set($"Просмотр элемента в {FileName}.xml");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");

            XmlNode ?FileNameNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}[id='" + id + "']");

            if (FileNameNode != null)
                switch (FileName)
                {
                    case "doctors":
                        string fullName = FileNameNode.SelectSingleNode("fullname-doctor").InnerText;
                        string birthday = FileNameNode.SelectSingleNode("birthday").InnerText;
                        string experience = FileNameNode.SelectSingleNode("experience").InnerText;
                        string animalsTreated = FileNameNode.SelectSingleNode("animals-treated").InnerText;
                        string telegramID = FileNameNode.SelectSingleNode("telegram-id").InnerText;

                        Console.WriteLine("Информация об враче:");
                        Console.WriteLine($"ФИО врача: {fullName} ID ({id})");
                        Console.WriteLine("Дата рождения: " + birthday);
                        Console.WriteLine("Опыт работы " + experience + " лет");
                        Console.WriteLine("Пациентов прошло данного врача: " + animalsTreated);
                        Console.WriteLine("Telegram ID: " + telegramID);
                        break; 
                    case "admission":
                        string pacienteId = FileNameNode.SelectSingleNode("paciente-id").InnerText;
                        string dateTime = FileNameNode.SelectSingleNode("date-time").InnerText;
                        string fullnameDoctor = FileNameNode.SelectSingleNode("fullname-doctor").InnerText;
                        string complaints = FileNameNode.SelectSingleNode("complaints").InnerText;
                        string diagnosis = FileNameNode.SelectSingleNode("diagnosis").InnerText;
                        string info = FileNameNode.SelectSingleNode("info").InnerText;

                        Console.WriteLine("Информация об приёме:");
                        Console.WriteLine($"ID пациента: {pacienteId} ID ({id})");
                        Console.WriteLine("Дата приёма: " + dateTime);
                        Console.WriteLine("ФИО врача " + fullnameDoctor);
                        Console.WriteLine("Жалобы пациента: " + complaints);
                        Console.WriteLine("Диагноз: " + diagnosis);
                        Console.WriteLine($"Доп. информация: {info}");
                        break;     
                    case "pacientes":
                        string animalType = FileNameNode.SelectSingleNode("paciente-id").InnerText;
                        string name = FileNameNode.SelectSingleNode("date-time").InnerText;
                        string gender = FileNameNode.SelectSingleNode("fullname-doctor").InnerText;
                        string age = FileNameNode.SelectSingleNode("complaints").InnerText;
                        string fullnameOwner = FileNameNode.SelectSingleNode("diagnosis").InnerText;
                        string valid = FileNameNode.SelectSingleNode("info").InnerText;
                        telegramID = FileNameNode.SelectSingleNode("telegram-id").InnerText;

                        Console.WriteLine("Информация об пациенте:");
                        Console.WriteLine($"Тип животного: {animalType} ID ({id})");
                        Console.WriteLine("Кличка животного: " + name);
                        Console.WriteLine("Пол животного: " + gender);
                        Console.WriteLine("Возраст: " + age);
                        Console.WriteLine("ФИО владельца: " + fullnameOwner);
                        Console.WriteLine("Активен(?): " + valid);
                        Console.WriteLine($"Telegram владельца: {telegramID}");
                        break;
                    default:
                        Console.WriteLine("Ошибка вывода");
                        Title.Set("Ошибка");
                        Title.Wait();
                        break;
                }
        }

    }
}