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
    public class XmlChange
    {
        // Обновить данные по ID
        public static void Update(string FileName, string MainTag, string ObjectTag, int id)
        {
            Title.Set($"Редактируется: {FileName}.xml ({id})");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            XmlNode ?FileNameNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}" + "[id='" + id + "']");

            if (FileNameNode != null)
            {
                switch (FileName)
                {
                    case "doctors":
                        string? fullName, birthday, experience, animalsTreated, telegramID;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы редактируете врача с ID ({id}).");

                            Console.Write("Введите ФИО доктора: ");
                            fullName = Console.ReadLine();

                            Console.Write("Введите дату рождения (ДД.ММ.ГГГГ): ");
                            birthday = Console.ReadLine();

                            Console.Write("Введите стаж работы врача: ");
                            experience = Console.ReadLine();
                            
                            Console.Write("Введите количество пройденных клиентов у врача\nВвод: ");
                            animalsTreated = Console.ReadLine();

                            Console.Write("Введите Telegram ID врача: ");
                            telegramID = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(telegramID))
                                telegramID = "@" + telegramID;
                            else if (!telegramID.StartsWith("@"))
                                telegramID = "@" + telegramID;
                        } while (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(birthday) || string.IsNullOrWhiteSpace(experience) || string.IsNullOrWhiteSpace(animalsTreated) || string.IsNullOrWhiteSpace(telegramID));
                        
                        FileNameNode.SelectSingleNode("fullname-doctor").InnerText = fullName;
                        FileNameNode.SelectSingleNode("birthday").InnerText = birthday;
                        FileNameNode.SelectSingleNode("experience").InnerText = experience;
                        FileNameNode.SelectSingleNode("animals-treated").InnerText = animalsTreated;
                        FileNameNode.SelectSingleNode("telegram-id").InnerText = telegramID;

                        document.Save("./database/doctors.xml");
                        Console.Clear();
                        Title.Set($"Успех!");
                        Console.WriteLine($"Доктор изменён в базе данных под ID ({id})");
                        Title.Wait();
                        break;
                    case "admission":
                        string? pacienteId, dateTime, fullnameDoctor, complaints, diagnosis;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы редактируете приём с ID ({id}).");

                            Console.Write("Введите ID пациента: ");
                            pacienteId = Console.ReadLine();

                            Console.Write("Введите дату приёма (ДД.ММ.ГГГГ): ");
                            dateTime = Console.ReadLine();

                            Console.Write("Введите ФИО врача: ");
                            fullnameDoctor = Console.ReadLine();
                            
                            Console.Write("Введите жалобы пациента: ");
                            complaints = Console.ReadLine();

                            Console.Write("Введите диагноз: ");
                            diagnosis = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(pacienteId) || string.IsNullOrWhiteSpace(dateTime) || string.IsNullOrWhiteSpace(fullnameDoctor) || string.IsNullOrWhiteSpace(complaints) || string.IsNullOrWhiteSpace(diagnosis));
                        
                        FileNameNode.SelectSingleNode("paciente-id").InnerText = pacienteId;
                        FileNameNode.SelectSingleNode("date-time").InnerText = dateTime;
                        FileNameNode.SelectSingleNode("fullname-doctor").InnerText = fullnameDoctor;
                        FileNameNode.SelectSingleNode("complaints").InnerText = complaints;
                        FileNameNode.SelectSingleNode("diagnosis").InnerText = diagnosis;

                        document.Save("./database/admission.xml");
                        Console.Clear();
                        Console.WriteLine($"Приём изменен в базе данных под ID ({id})");
                        Title.Set("Успех!");
                        Title.Wait();
                        break;
                    case "paciente":
                        string? animalType, name, gender, age, fullnameOwner;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы редактируете пациента с ID ({id}).");

                            Console.Write("Введите тип животного: ");
                            animalType = Console.ReadLine();

                            Console.Write("Введите кличку животного: ");
                            name = Console.ReadLine();
                            
                            Console.Write("Введите пол животного: ");
                            gender = Console.ReadLine();

                            Console.Write("Введите возраст животного: ");
                            age = Console.ReadLine();

                            Console.Write("Введите ФИО владельца животного: ");
                            fullnameOwner = Console.ReadLine();

                            Console.Write("Введите Telegram владельца: ");
                            telegramID = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(telegramID))
                                telegramID = "@" + telegramID;
                            else if (!telegramID.StartsWith("@"))
                                telegramID = "@" + telegramID;
                        } while (string.IsNullOrWhiteSpace(animalType) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(fullnameOwner) || string.IsNullOrWhiteSpace(telegramID));
                        
                        FileNameNode.SelectSingleNode("animal-type").InnerText = animalType;
                        FileNameNode.SelectSingleNode("name").InnerText = name;
                        FileNameNode.SelectSingleNode("gender").InnerText = gender;
                        FileNameNode.SelectSingleNode("age").InnerText = age;
                        FileNameNode.SelectSingleNode("fullnameOwner").InnerText = fullnameOwner;
                        FileNameNode.SelectSingleNode("telegram-id").InnerText = telegramID;

                        document.Save("./database/paciente.xml");
                        Console.Clear();
                        Console.WriteLine($"Пациент изменён базе данных под ID ({id})");
                        Title.Set("Успех!");
                        Title.Wait();
                        break;
                    default:
                        Title.Set("Ошибка");
                        Console.WriteLine("[DEBUG] Неверный файл");
                        Title.Wait();
                        break;
                }
            }
            else
            {
                Title.Set("Ошибка");
                Console.WriteLine("Программа не нашла данного врада, возможно под данным ID нет врача.");
                Title.Wait();
            }
        }
        
        // Отдельное обновление информации у Приёмов, ибо информация требует больше текста
        public static void UpdateInfo(string id) 
        {
            Title.Set($"Редактируется: admission.xml ({id})");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/admission.xml");
            XmlNode ?admissionNode = document.SelectSingleNode($"/admission/animal[id='{id}']");

            if (admissionNode != null)
            {
                string? info;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Вы редактируете Приём ID ({id})");

                    Console.WriteLine("Введите изменённую информацию об данном приёме, информация полностью переписывается\nЕсли нужно, то стоит написать предыдущую информацию, чтобы её дополнить.");
                    Console.WriteLine("-----------------\nПредыдущая информация:\n" + admissionNode.SelectSingleNode("fullname-doctor").InnerText + "\n-----------------");
                    Console.Write("Начните ввод здесь: ");
                    info = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(info));
                admissionNode.SelectSingleNode("info").InnerText = info;
                document.Save("./database/admission.xml");
                Console.Clear();
                Console.WriteLine($"Информация об приёме изменена в базе данных под ID ({id})");
                Title.Set("Успех!");
                Title.Wait();
            }
        }
        public static void UpdateValid(string id) 
        {
            Title.Set($"Редактируется: pacientes.xml ({id})");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/pacientes.xml");
            XmlNode ?admissionNode = document.SelectSingleNode($"/pacientes/paciente[id='{id}']");

            if (admissionNode != null)
            {
                string? valid;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Вы редактируете ID Пациента ({id})");

                    Console.WriteLine("Введите валидность Пациента [Да / Нет]: ");
                    do
                        valid = Console.ReadLine();
                    while (valid == null || valid.ToLower() != "да" || valid.ToLower() != "нет");
                } while (string.IsNullOrWhiteSpace(valid));

                XmlDocument doctor = new XmlDocument();
                doctor.Load($"./database/doctors.xml");
                XmlNode ?doctorNode = document.SelectSingleNode($"/doctors/doctor[fullname-doctor='{admissionNode.SelectSingleNode("fullname-doctor").InnerText}']");
                if (doctorNode != null)
                {
                    if (valid.ToLower() == "да")
                    {
                        valid = "Да";
                        doctorNode.SelectSingleNode("animals-treated").InnerText = (Convert.ToInt32(doctorNode.SelectSingleNode("animals-treated").InnerText) + 1).ToString();
                    }
                    else if (valid.ToLower() == "нет")
                    {
                        valid = "Нет";
                        doctorNode.SelectSingleNode("animals-treated").InnerText = (Convert.ToInt32(doctorNode.SelectSingleNode("animals-treated").InnerText) + 1).ToString();
                    }
                }

                admissionNode.SelectSingleNode("valid").InnerText = valid;
                document.Save("./database/pacientes.xml");
                doctor.Save("./database/doctors.xml");
                Console.Clear();
                Console.WriteLine($"Информация об приёме изменена в базе данных под ID ({id})");
                Title.Set("Успех!");
                Title.Wait();
            }
        }
    }
}