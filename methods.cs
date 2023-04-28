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
            document.Load($"./database/{FileName}.xml");
            XmlNodeList ?nodeList = document.SelectNodes($"/{MainTag}/{ObjectTag}");
            int currentPage = 1; // Текущая страница
            int pageSize = 2; // Количество записей на странице
            if (FileName == "statistic" || FileName == "admission")
                pageSize = 1;
            int totalPages = (int)Math.Ceiling((double)document.SelectNodes($"/{MainTag}/{ObjectTag}").Count / pageSize); // Общее количество страниц
            
            bool finished = false;

            while (!finished)
            {
                Console.Clear();
                Console.WriteLine("Страница {0} из {1}", currentPage, totalPages);
                string input;
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
                                    "ФИО доктора: " + FileNameNode.SelectSingleNode("fullname-doctor")?.InnerText + "\n" +
                                    "Дата рождения: " + FileNameNode.SelectSingleNode("birthday")?.InnerText + "\n" +
                                    "Опыт работы " + FileNameNode.SelectSingleNode("experience")?.InnerText + " лет" + "\n" +
                                    "Пациентов прошло данного врача:  " + FileNameNode.SelectSingleNode("animals-treated")?.InnerText + "\n" +
                                    "Telegram ID:  " + FileNameNode.SelectSingleNode("telegram-id")?.InnerText + " (" + FileNameNode.SelectSingleNode("id")?.InnerText + ")\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine(
                                "Чтобы посмотреть следующую страницу введите 'n'\n" +
                                "Введите 'p' Чтобы просмотреть предыдущую страницу, \n" +
                                "Любая другая клавиша выводит вас с данного меню");
                            do
                                input = Console.ReadLine();
                            while (input == null);

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
                        case "admission":
                            Console.WriteLine("-----------------------------");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    "Дата: " + FileNameNode.SelectSingleNode("dateTime")?.InnerText + " ID (" + FileNameNode.SelectSingleNode("id")?.InnerText + ")\n" +
                                    "ФИО доктора: " + FileNameNode.SelectSingleNode("fullname-doctor")?.InnerText + "\n" +
                                    "Индификатор пациента: ID (" + FileNameNode.SelectSingleNode("paciente-id")?.InnerText + "\n" +
                                    "Жалобы: " + FileNameNode.SelectSingleNode("complaints")?.InnerText + ")\n" +
                                    "Диагноз:  " + FileNameNode.SelectSingleNode("diagnosis")?.InnerText + "\n" +
                                    "Дополнительная информация от доктора:  " + FileNameNode.SelectSingleNode("info")?.InnerText + "\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine(
                                "Чтобы посмотреть следующую страницу введите 'n'\n" +
                                "Введите 'p' Чтобы просмотреть предыдущую страницу, \n" +
                                "Любая другая клавиша выводит вас с данного меню");
                            do
                                input = Console.ReadLine();
                            while (input == null);

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
                        case "pacientes":
                            Console.WriteLine("-----------------------------");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    "Тип животного: " + FileNameNode.SelectSingleNode("animal-type")?.InnerText + " ID (" + FileNameNode.SelectSingleNode("id")?.InnerText + ")\n" +
                                    "Кличка: " + FileNameNode.SelectSingleNode("name")?.InnerText + "\n" +
                                    "Пол: " + FileNameNode.SelectSingleNode("gender")?.InnerText + "\n" +
                                    "Возраст животного:  " + FileNameNode.SelectSingleNode("age")?.InnerText + "\n" +
                                    "ФИО владельца:  " + FileNameNode.SelectSingleNode("info")?.InnerText + "\n" +
                                    "Телеграм владельца: " + FileNameNode.SelectSingleNode("telegram-id")?.InnerText + "\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine(
                                "Чтобы посмотреть следующую страницу введите 'n'\n" +
                                "Введите 'p' Чтобы просмотреть предыдущую страницу, \n" +
                                "Любая другая клавиша выводит вас с данного меню");
                            do
                                input = Console.ReadLine();
                            while (input == null);

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
                        case "statistic":
                            Console.WriteLine("-----------------------------");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    "Дата: " + FileNameNode.SelectSingleNode("date")?.InnerText + "\n" +
                                    "Описание данного дня: " + FileNameNode.SelectSingleNode("description")?.InnerText + "\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine(
                                "Чтобы посмотреть следующую страницу введите 'n'\n" +
                                "Введите 'p' Чтобы просмотреть предыдущую страницу, \n" +
                                "Любая другая клавиша выводит вас с данного меню");
                            do
                                input = Console.ReadLine();
                            while (input == null);

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
                            Console.WriteLine("[DEBUG] Неверно указан файл");
                            break;
                    }
                else
                {
                    Console.WriteLine("[DEBUG] Ошибка чтения");
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
                        Console.WriteLine($"Доктор {fullName} добавлен в базу данных под ID ({id})");
                        break;
                    
                    default:
                        Console.WriteLine("[DEBUG] Неверный файл");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Программа не нашла данного врада, возможно под данным ID нет врача.");
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
                    id = GetMaxXMLId(FileName).ToString();
                    do 
                    {
                        Console.Clear();
                        Console.WriteLine($"Вы добавляете нового врача, его ID будет: ({id})");

                        Console.Write("Введите ФИО врача: ");
                        fullName = Console.ReadLine();

                        Console.Write("Введите дату рождения (ДД.ММ.ГГГГ): ");
                        birthday = Console.ReadLine();

                        Console.Write("Введите стаж работы врача: ");
                        experience = Console.ReadLine();
                        
                        Console.Write("Введите количество пройденных клиентов у врача\nВвод: ");
                        animalsTreated = Console.ReadLine();

                        Console.Write("|inputTgID|");
                        telegramID = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(telegramID))
                                telegramID = "@" + telegramID;
                            else if (!telegramID.StartsWith("@"))
                                telegramID = "@" + telegramID;
                    } while (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(birthday) || string.IsNullOrWhiteSpace(experience) || string.IsNullOrWhiteSpace(animalsTreated) || string.IsNullOrWhiteSpace(telegramID));

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
                    Console.Clear();
                    Console.WriteLine($"Новый профиль врача был создан, его ID ({id})");
                    break;
                    default:
                    Console.WriteLine("[DEBUG] Неверный файл");
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
                    Console.WriteLine("[DEBUG] |errorInside|");
                
                document.Save($"./database/{FileName}");
                Console.WriteLine($"| {id}, {FileName}|Успешно удалена секцая под ID: {id}, в файле {FileName}.xml");
            }
            else
            {
                Console.WriteLine($"|notFoundSection {id} {FileName}|");
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

                        Console.WriteLine("|ID|" + id);
                        Console.WriteLine("|fullNameDoc|" + fullName);
                        Console.WriteLine("|dateBorn|" + birthday);
                        Console.WriteLine("|exp|" + experience);
                        Console.WriteLine("|pastClients|" + animalsTreated);
                        Console.WriteLine("Telegram ID: " + telegramID);
                    }
                    break;                    
                    default:
                    Console.WriteLine("|errorOutput|");
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

                    Console.WriteLine("|ID|" + id);
                    Console.WriteLine("|fullNameDoc|" + fullName);
                    Console.WriteLine("|dateBorn|" + birthday);
                    Console.WriteLine("|exp|" + experience);
                    Console.WriteLine("|pastClients|" + animalsTreated);
                    Console.WriteLine("Telegram ID: " + telegramID);
                    break;                    
                    default:
                    Console.WriteLine("|errorOutput|");
                    break;
                }
        }
        static void wait()
        {
            Console.Write("|anyKey|");
            Console.ReadKey();
        }
    }
}