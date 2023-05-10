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
                        string? fullName, birthday, animalsTreated, telegramID;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы редактируете врача с ID ({id}).");
                            Title.Wait();

                            fullName = Valid.FullNameUser("врача");
                            birthday = Valid.Date("рождения врача");
                            animalsTreated = Valid.Number("Введите количество пройденных клиентов у врача: ");
                            telegramID = Valid.TelegramID("Введите Telegram врача: ");

                        } while (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(birthday) || string.IsNullOrWhiteSpace(animalsTreated) || string.IsNullOrWhiteSpace(telegramID));
                        
                        FileNameNode.SelectSingleNode("fullname-doctor").InnerText = fullName;
                        FileNameNode.SelectSingleNode("birthday").InnerText = birthday;
                        FileNameNode.SelectSingleNode("animals-treated").InnerText = animalsTreated;
                        FileNameNode.SelectSingleNode("telegram-id").InnerText = telegramID;

                        document.Save("./database/doctors.xml");
                        Console.Clear();
                        Title.Set($"Успех!");
                        Console.WriteLine($"Доктор изменён в базе данных под ID ({id})");
                        Title.Wait();
                        break;
                    case "admission":
                        string? pacienteId, dateTime, fullnameDoctor, complaints, diagnosis, time;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы редактируете приём с ID ({id}).");
                            Title.Wait();

                            while (true)
                            {
                                pacienteId = Valid.Number("Введите ID пациента: ");
                                XDocument doc = XDocument.Load("./database/pacientes.xml");
                                // Поиск и просмотр элемента в БД по ID
                                bool idExists = doc.Descendants("id").Any(x => (string)x == pacienteId);

                                if (idExists)
                                    break;
                                else
                                {
                                    Console.Clear();
                                    Console.Write("Данный пациент отсутствует в базе данных. \n(Вы желаете попробовать снова? ('y' - да, 'другой ответ' - нет)\nВвод: ");
                                    string answer = Console.ReadLine();
                                    if (answer.ToLower() != "д" || answer.ToLower() != "да")
                                        return;
                                }
                            }
                            dateTime = Valid.Date("приёма");

                            Console.Clear();
                            Console.WriteLine("Введите время приёма (ЧЧ:ММ): ");
                            time = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(time))
                                time = "00:00";

                            while (true)
                            {
                                fullnameDoctor = Valid.FullNameUser("доктора");
                                XDocument doc = XDocument.Load("./database/pacientes.xml");
                                // Поиск и просмотр элемента в БД по ID
                                bool idExists = doc.Descendants("fullname-doctor").Any(x => (string)x == fullnameDoctor);

                                if (idExists)
                                    break;
                                else
                                {
                                    Console.Clear();
                                    Console.Write("Данный врач отсутствует в базе данных. \n(Вы желаете попробовать снова? ('y' - да, 'другой ответ' - нет)\nВвод: ");
                                    string answer = Console.ReadLine();
                                    if (answer.ToLower() != "д" || answer.ToLower() != "да")
                                        return;
                                }
                            }
                            
                            Console.Clear();
                            Console.Write("Введите жалобы пациента: ");
                            complaints = Console.ReadLine();

                            Console.Clear();
                            Console.Write("Введите диагноз: ");
                            diagnosis = Console.ReadLine();
                            
                            Console.Clear();
                            Console.WriteLine("Чтобы изменить информацию о приёме, используйте соответствующий пункт программы");
                            Title.Wait();
                        } while (string.IsNullOrWhiteSpace(pacienteId) || string.IsNullOrWhiteSpace(dateTime) || string.IsNullOrWhiteSpace(fullnameDoctor) || string.IsNullOrWhiteSpace(complaints) || string.IsNullOrWhiteSpace(diagnosis));
                        
                        FileNameNode.SelectSingleNode("paciente-id").InnerText = pacienteId;
                        FileNameNode.SelectSingleNode("date-time").InnerText = dateTime;
                        FileNameNode.SelectSingleNode("fullname-doctor").InnerText = fullnameDoctor;
                        FileNameNode.SelectSingleNode("complaints").InnerText = complaints;
                        FileNameNode.SelectSingleNode("diagnosis").InnerText = diagnosis;
                        FileNameNode.SelectSingleNode("time").InnerText = time;

                        document.Save("./database/admission.xml");
                        Console.Clear();
                        Console.WriteLine($"Приём изменен в базе данных под ID ({id})");
                        Title.Set("Успех!");
                        Title.Wait();
                        break;
                    case "pacientes":
                        string? name, age;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы редактируете пациента с ID ({id}).");
                            Title.Wait();

                            Console.Clear();
                            Console.Write("Введите кличку животного: ");
                            name = Console.ReadLine();

                            age = Valid.Number("Введите возраст животного: ");
                        } while (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(age));
                        
                        FileNameNode.SelectSingleNode("name").InnerText = name;
                        FileNameNode.SelectSingleNode("age").InnerText = age;

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
            XmlNode admissionNode = document.SelectSingleNode($"/admission/animal[id='{id}']");

            if (admissionNode != null)
            {
                string? info;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Вы редактируете Приём ID ({id})");

                    Console.WriteLine("Введите изменённую информацию об данном приёме, информация полностью переписывается\nЕсли нужно, то стоит написать предыдущую информацию, чтобы её дополнить.");
                    Console.WriteLine("-----------------\nПредыдущая информация:\n" + admissionNode.SelectSingleNode("info").InnerText + "\n-----------------");
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