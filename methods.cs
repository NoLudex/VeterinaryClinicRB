using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;

namespace VeterinaryClinicRB
{
    
    partial class __main__
    {
        // Здесь хранятся все методы приложения
        // Вывод данных в список:
        static void ReadXML(string FileName, string MainTag, string ObjectTag)
        {
            XmlDocument document = new XmlDocument();
            document.Load("./database/" + FileName + ".xml");
            XmlNodeList ?nodeList = document.SelectNodes("/" + MainTag + "/" + ObjectTag);
            int currentPage = 1; // Текущая страница
            int pageSize = 2; // Количество записей на странице
            int totalPages = (int)Math.Ceiling((double)document.SelectNodes($"/{MainTag}/{ObjectTag}").Count / pageSize); // Общее количество страниц
            
            bool finished = false;

            while (!finished)
            {
                Console.Clear();
                Console.WriteLine("Страница {0} из {1}\n", currentPage, totalPages);

                XmlNodeList FileNameNodes = document.SelectNodes($"/{MainTag}/{ObjectTag}[position()>={(currentPage -1) * pageSize + 1} and position()<={currentPage * pageSize}]");
                if (nodeList != null)
                    switch (FileName)
                    {
                        // Доступные файлы для работы данного метода:
                        case "doctors":
                            Console.WriteLine("-----------------------------");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    "ID: " + FileNameNode.SelectSingleNode("id")?.InnerText + "\n" +
                                    "ФИО врача: " + FileNameNode.SelectSingleNode("fullname-doctor")?.InnerText + "\n" +
                                    "Дата рождения: " + FileNameNode.SelectSingleNode("birthday")?.InnerText + "\n" +
                                    "Опыт работы: " + FileNameNode.SelectSingleNode("experience")?.InnerText + " лет" + "\n" +
                                    "Пациентов прошло данного врача: " + FileNameNode.SelectSingleNode("animals-treated")?.InnerText + "\n" +
                                    "Telegram ID: " + FileNameNode.SelectSingleNode("telegram-id")?.InnerText + "\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine("Нажмите 'p' Чтобы просмотреть предыдущую страницу, \nЧтобы посмотреть следующую страницу нажми 'n'\nЛюбая другая клавиша выводит вас с данного меню");
                            string input = Console.ReadLine();

                            switch (input.ToLower())
                            {
                                case "p":
                                    if (currentPage > 1)
                                    {
                                        currentPage--;
                                    }
                                    break;
                                case "n":
                                    if (currentPage < totalPages)
                                    {
                                        currentPage++;
                                    }
                                    break;
                                default:
                                    finished = true;
                                    break;
                            }
                            
                            break;
                        default:
                            Console.WriteLine("[DEBUG] Данный файл для БД не поддерживается");
                            break;
                    }
                else
                {
                    Console.WriteLine("[DEBUG] Ошибка с чтением XML");
                }
            }
        }

        // Обновить данные по ID
        static void UpdateXML (string FileName, string MainTag, string ObjectTag, int id)
        {
            XmlDocument document = new XmlDocument();
            document.Load("./database/" + FileName + ".xml");
            XmlNode ?FileNameNode = document.SelectSingleNode("/" + MainTag + "/" + ObjectTag + "[id='" + id + "']");

            if (FileNameNode != null)
            {
                switch (FileName)
                {
                    case "doctors":
                        string? fullName, birthday, experience, animalsTreated, telegramID;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Открыт редактор профиля ID {id}");

                            Console.Write("Введите ФИО доктора: ");
                            fullName = Console.ReadLine();

                            Console.Write("Введите дату рождения (ДД.ММ.ГГГГ): ");
                            birthday = Console.ReadLine();

                            Console.Write("Введите опыт работы: ");
                            experience = Console.ReadLine();
                            
                            Console.Write("Введите число пациентов, которые прошли данного врача" + "\n" + "Ввод: ");
                            animalsTreated = Console.ReadLine();

                            Console.Write("Введите Telegram пользователя: ");
                            telegramID = Console.ReadLine();
                        } while (fullName == null || birthday == null || experience == null || animalsTreated == null || telegramID == null);
                        
                        FileNameNode.SelectSingleNode("fullname-doctor").InnerText = fullName;
                        FileNameNode.SelectSingleNode("birthday").InnerText = birthday;
                        FileNameNode.SelectSingleNode("experience").InnerText = experience;
                        FileNameNode.SelectSingleNode("animals-treated").InnerText = animalsTreated;
                        FileNameNode.SelectSingleNode("telegram-id").InnerText = telegramID;

                        document.Save("./database/doctors.xml");
                        Console.Clear();
                        Console.WriteLine("Доктор с ID {id} был успешно обновлен");
                        break;
                    default:
                        Console.WriteLine("[DEBUG] Данный файл для БД не поддерживается");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Доктор с ID {id} не найден");
            }
        }
        
        // Добавить новый элемент в БД
        static void AddXML (string FileName, string MainTag, string ObjectTag)
        {
            XmlDocument document = new XmlDocument();
            document.Load("./database/" + FileName + ".xml");
            
            XmlNode ?root = document.SelectSingleNode($"/{MainTag}");
            XmlElement AddElement = document.CreateElement($"{ObjectTag}");

            switch (FileName)
                {
                    case "doctors":
                    string? id, fullName, birthday, experience, animalsTreated, telegramID;
                    id = Convert.ToString(GetMaxXMLId("doctor")); 
                    do 
                    {
                        Console.Clear();
                        Console.WriteLine($"Открыт редактор создания профиля");

                        Console.Write("Введите ФИО доктора: ");
                        fullName = Console.ReadLine();

                        Console.Write("Введите дату рождения (ДД.ММ.ГГГГ): ");
                        birthday = Console.ReadLine();

                        Console.Write("Введите опыт работы: ");
                        experience = Console.ReadLine();
                        
                        Console.Write("Введите число пациентов, которые прошли данного врача" + "\n" + "Ввод: ");
                        animalsTreated = Console.ReadLine();

                        Console.Write("Введите Telegram пользователя: ");
                        telegramID = Console.ReadLine();
                    } while (fullName == null || birthday == null || experience == null || animalsTreated == null || telegramID == null);

                    XmlElement idElement = document.CreateElement("id");
                    XmlElement fullNameElement = document.CreateElement("fullname-doctor");
                    XmlElement birthdayElement = document.CreateElement("birthday");
                    XmlElement experienceElement = document.CreateElement("experience");
                    XmlElement animalsTreatedElement = document.CreateElement("animals-treated");
                    XmlElement telegramIDElement = document.CreateElement("telegram-id");

                    idElement.InnerText = id;
                    fullNameElement.InnerText = fullName;
                    birthdayElement.InnerText = birthday;
                    experienceElement.InnerText = experience;
                    animalsTreatedElement.InnerText = animalsTreated;
                    telegramIDElement.InnerText = telegramID;

                    AddElement.AppendChild(idElement);
                    AddElement.AppendChild(fullNameElement);
                    AddElement.AppendChild(birthdayElement);
                    AddElement.AppendChild(experienceElement);
                    AddElement.AppendChild(animalsTreatedElement);
                    AddElement.AppendChild(telegramIDElement);

                    root?.AppendChild(AddElement);
                    document.Save($"./database/{FileName}.xml");
                    int Abuz = Convert.ToInt32(id) + 1;
                    ConfigurationManager.AppSettings.Set("MaxDoctorId", Abuz.ToString());
                    Console.Clear();
                    Console.WriteLine("Новый доктор был успешно добавлен.");
                    break;
                    default:
                    Console.WriteLine("[DEBUG] Данный файл для БД не поддерживается");
                    break;
                }
        }

        // Удаление Элемента
        static void DeleteXML (string FileName, string MainTag, string ObjectTag, int id)
        {
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            XmlNode ?FileNameNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}[id='{id}']");

            if (FileNameNode != null)
            {
                XmlNode ?parentNode = FileNameNode.ParentNode;
                if (parentNode != null)
                    parentNode.RemoveChild(FileNameNode);
                else
                    Console.WriteLine("[DEBUG] Произошла внутренняя ошибка");
                
                document.Save($"./database/{FileName}");
                Console.WriteLine($"Успешно удалена секцая под ID: {id}, в файле {FileName}.xml");
            }
            else
            {
                Console.WriteLine($"Секция под ID: {id}, в файле {FileName} не найдена");
            }
        }

        // Показать все элементы XML
        static void ShowAllXML(string FileName, string MainTag, string ObjectTag)
        {
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");

            XmlNodeList ?FileNameNodes = document.SelectNodes($"/{MainTag}/{ObjectTag}");
            if (FileNameNodes != null)
                switch (FileName)
                {
                    case "doctors":
                    foreach (XmlNode FileNameNode in FileNameNodes)
                    {
                        string id = FileNameNode.SelectSingleNode("id").InnerText;
                        string fullName = FileNameNode.SelectSingleNode("fullname-doctor").InnerText;
                        string birthday = FileNameNode.SelectSingleNode("birthday").InnerText;
                        string experience = FileNameNode.SelectSingleNode("experience").InnerText;
                        string animalsTreated = FileNameNode.SelectSingleNode("animalsTreated").InnerText;
                        string telegramID = FileNameNode.SelectSingleNode("telegram-id").InnerText;

                        Console.WriteLine("ID: " + id);
                        Console.WriteLine("ФИО доктора: " + fullName);
                        Console.WriteLine("Дата рождения: " + birthday);
                        Console.WriteLine("Опыт работы: " + experience);
                        Console.WriteLine("Проверенные клиенты: " + animalsTreated);
                        Console.WriteLine("Telegram ID: " + telegramID);
                    }
                    break;                    
                    default:
                    Console.WriteLine("Невозможно вывести данные");
                    break;
                }
        }
        
        // // Вывести информацию об одном элементе
        // static void ShowXMLByID(string FileName, string MainTag, string ObjectTag, int id)
        // {
        //     XmlDocument document = new XmlDocument();
        //     document.Load($"./database/{FileName}.xml");

        //     XmlNode ?doctorNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}[id='{id}']");

        //     if(doctorNode != null)
        //     {
        //         string id = doctorNode.SelectSingleNode("id").InnerText;
        //         string fullName = doctorNode.SelectSingleNode("fullname-doctor").InnerText;
        //         string birthday = doctorNode.SelectSingleNode("birthday").InnerText;
        //         string specialization = doctorNode.SelectSingleNode("specialization").InnerText;

        //         Console.WriteLine("ID: " + id);
        //         Console.WriteLine("Full Name: " + fullName);
        //         Console.WriteLine("Birthday: " + birthday);
        //         Console.WriteLine("Specialization: " + specialization);
        //     }
        //     else
        //     {
        //         Console.WriteLine("Doctor with ID " + idToFind + " not found.");
        //     }
        // }
        static void ShowXMLByID(string FileName, string MainTag, string ObjectTag, int id)
        {
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

                    Console.WriteLine("ID: " + id);
                    Console.WriteLine("ФИО доктора: " + fullName);
                    Console.WriteLine("Дата рождения: " + birthday);
                    Console.WriteLine("Опыт работы: " + experience);
                    Console.WriteLine("Проверенные клиенты: " + animalsTreated);
                    Console.WriteLine("Telegram ID: " + telegramID);
                    break;                    
                    default:
                    Console.WriteLine("Невозможно вывести данные");
                    break;
                }
        }
        static void wait()
        {
            Console.Write("Нажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
        }
    }
}