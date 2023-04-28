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
                Title.Set($"Страница {currentPage} из {totalPages}");
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
                                    "Дата: " + FileNameNode.SelectSingleNode("date-time")?.InnerText + " ID (" + FileNameNode.SelectSingleNode("id")?.InnerText + ")\n" +
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
                            Title.Set($"Ошибка");
                            Console.WriteLine("[DEBUG] Неверно указан файл");
                            wait();
                            break;
                    }
                else
                {
                    Title.Set($"Ошибка");
                    Console.WriteLine("[DEBUG] Ошибка чтения");
                    wait();
                }
            }
        }

        // Обновить данные по ID
        static void UpdateXML (string FileName, string MainTag, string ObjectTag, int id)
        {
            Title.Set($"Редактируется: {FileName}.xml ({id})");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            XmlNode ?FileNameNode = document.SelectSingleNode("/{MainTag}/{ObjectTag}" + "[id='" + id + "']");

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
                        wait();
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
                        Title.Set("Успешно!");
                        wait();
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
                        Title.Set("Успешно!");
                        wait();
                        break;
                    // case "statistic":
                    //     string? date, description;
                    //     do 
                    //     {
                    //         Console.Clear();
                    //         Console.WriteLine($"Вы редактируете приём с ID ({id}).");

                    //         Console.Write("Введите ID пациента: ");
                    //         date = Console.ReadLine();

                    //         Console.Write("Введите дату приёма (ДД.ММ.ГГГГ): ");
                    //         description = Console.ReadLine();
                    //     } while (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(description));
                        
                    //     FileNameNode.SelectSingleNode("date").InnerText = date;
                    //     FileNameNode.SelectSingleNode("description").InnerText = description;

                    //     document.Save("./database/doctors.xml");
                    //     Console.Clear();
                    //     Console.WriteLine($"Статистика изменён базе данных под ID ({id})");
                    //     break;
                    default:
                        Title.Set("Ошибка");
                        Console.WriteLine("[DEBUG] Неверный файл");
                        wait();
                        break;
                }
            }
            else
            {
                Title.Set("Ошибка");
                Console.WriteLine("Программа не нашла данного врада, возможно под данным ID нет врача.");
                wait();
            }
        }
        
        // Добавить новый элемент в БД
        static void AddXML (string FileName, string MainTag, string ObjectTag)
        {
            Title.Set($"Добавление нового элемента {FileName}.xml");
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

                            Console.Write("Введите Telegram врача: ");
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
                        Title.Set("Успех");
                        wait();
                        break;
                    case "admission":
                        string? pacienteId, dateTime, fullnameDoctor, complaints, diagnosis, info;
                        id = GetMaxXMLId(FileName).ToString();
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы добавляете новый приём, его ID будет: ({id})");

                            Console.Write("Введите ID пациента: ");
                            pacienteId = Console.ReadLine();

                            Console.Write("Введите дату приёма (ДД.ММ.ГГГГ): ");
                            dateTime = Console.ReadLine();

                            Console.Write("Введите ФИО доктора: ");
                            fullnameDoctor = Console.ReadLine();
                            
                            Console.Write("Введите жалобы: ");
                            complaints = Console.ReadLine();

                            Console.Write("Введите диагноз: ");
                            diagnosis = Console.ReadLine();

                            Console.Write("Введите доп. информацию об приёме: ");
                            info = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(pacienteId) || string.IsNullOrWhiteSpace(dateTime)  || string.IsNullOrWhiteSpace(info) || string.IsNullOrWhiteSpace(fullnameDoctor) || string.IsNullOrWhiteSpace(complaints) || string.IsNullOrWhiteSpace(diagnosis));

                        XmlElement Element1 = document.CreateElement("id");
                        XmlElement Element2 = document.CreateElement("paciente-id");
                        XmlElement Element3 = document.CreateElement("date-time");
                        XmlElement Element4 = document.CreateElement("fullname-doctor");
                        XmlElement Element5 = document.CreateElement("complaints");
                        XmlElement Element6 = document.CreateElement("diagnosis");
                        XmlElement Element7 = document.CreateElement("info");

                        Element1.InnerText = id;
                        Element2.InnerText = pacienteId;
                        Element3.InnerText = dateTime;
                        Element4.InnerText = fullnameDoctor;
                        Element5.InnerText = complaints;
                        Element6.InnerText = diagnosis;
                        Element7.InnerText = info;

                        AddElement.AppendChild(Element1);
                        AddElement.AppendChild(Element2);
                        AddElement.AppendChild(Element3);
                        AddElement.AppendChild(Element4);
                        AddElement.AppendChild(Element5);
                        AddElement.AppendChild(Element6);
                        AddElement.AppendChild(Element7);


                        root?.AppendChild(AddElement);
                        document.Save($"./database/{FileName}.xml");
                        Abuz = Convert.ToInt32(id) + 1;
                        Console.Clear();
                        Console.WriteLine($"Новые данные об приёме были созданы, ID ({id})");
                        Title.Set("Успех");
                        wait();
                        break;
                    case "pacientes":
                        string? animalType, name, gender, age, fullnameOwner, valid;
                        id = GetMaxXMLId(FileName).ToString();
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы добавляете новый приём, его ID будет: ({id})");

                            Console.Write("Введите тип животного: ");
                            animalType = Console.ReadLine();

                            Console.Write("Введите кличку животного: ");
                            name = Console.ReadLine();

                            Console.Write("Введите ФИО доктора: ");
                            fullnameDoctor = Console.ReadLine();
                            
                            Console.Write("Введите пол животного: ");
                            gender = Console.ReadLine();

                            Console.Write("Введите возраст животного: ");
                            age = Console.ReadLine();

                            Console.Write("Введите ФИО владельца: ");
                            fullnameOwner = Console.ReadLine();

                            Console.Write("Валиден ли данный пациент? (Да / Нет): ");
                            do
                                valid = Console.ReadLine();
                            while (valid.ToLower() != "да" || valid.ToLower() != "нет");

                            Console.Write("Введите Telegram ID владельца: ");
                            telegramID = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(telegramID))
                                telegramID = "@" + telegramID;
                            else if (!telegramID.StartsWith("@"))
                                telegramID = "@" + telegramID;
                        } while (string.IsNullOrWhiteSpace(animalType) || string.IsNullOrWhiteSpace(name)  || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(fullnameOwner) || string.IsNullOrWhiteSpace(valid) || string.IsNullOrWhiteSpace(telegramID));

                        Element1 = document.CreateElement("id");
                        Element2 = document.CreateElement("animal-type");
                        Element3 = document.CreateElement("name");
                        Element4 = document.CreateElement("gender");
                        Element5 = document.CreateElement("age");
                        Element6 = document.CreateElement("fullname-owner");
                        Element7 = document.CreateElement("valid");
                        XmlElement Element8 = document.CreateElement("telegram-id");

                        Element1.InnerText = id;
                        Element2.InnerText = animalType;
                        Element3.InnerText = name;
                        Element4.InnerText = gender;
                        Element5.InnerText = age;
                        Element6.InnerText = fullnameOwner;
                        Element7.InnerText = valid;
                        Element8.InnerText = telegramID;

                        AddElement.AppendChild(Element1);
                        AddElement.AppendChild(Element2);
                        AddElement.AppendChild(Element3);
                        AddElement.AppendChild(Element4);
                        AddElement.AppendChild(Element5);
                        AddElement.AppendChild(Element6);
                        AddElement.AppendChild(Element7);
                        AddElement.AppendChild(Element8);


                        root?.AppendChild(AddElement);
                        document.Save($"./database/{FileName}.xml");
                        Abuz = Convert.ToInt32(id) + 1;
                        Console.Clear();
                        Console.WriteLine($"Новый профиль пациента был добавлен, его ID ({id})");
                        Title.Set("Успех");
                        wait();
                        break;
                    case "statistic":
                        string? date, description;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы добавляете новое событие дня");

                            Console.Write("Напишите дату события: ");
                            date = Console.ReadLine();

                            Console.Write("Напишите что произошло (Описание): ");
                            description = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(description));

                        Element1 = document.CreateElement("date");
                        Element2 = document.CreateElement("description");

                        Element1.InnerText = date;
                        Element2.InnerText = description;

                        AddElement.AppendChild(Element1);
                        AddElement.AppendChild(Element2);

                        root?.AppendChild(AddElement);
                        document.Save($"./database/{FileName}.xml");
                        Console.Clear();
                        Console.WriteLine($"Новое событие было добавлено на число: {date}");
                        XmlSort.SortByDate("./database/statistic.xml");
                        Title.Set("Успех");
                        wait();
                        break;
                    default:
                        Title.Set("Ошибка");
                        Console.WriteLine("[DEBUG] Неверный файл");
                        wait();
                        break;
                }
        }

        // Удаление Элемента
        static void DeleteXML (string FileName, string MainTag, string ObjectTag, int id)
        {   
            Title.Set($"Удаление элемента в {FileName}.xml");
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
                Title.Set("Успех");
                wait();
            }
            else
            {
                Console.WriteLine($"Не найдена секция под ID ({id}) в файле {FileName}.xml");
                Title.Set("Ошибка");
                wait();
            }
        }
        static void ShowXMLByID(string FileName, string MainTag, string ObjectTag, int id)
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
                        wait();
                        break;
                }
        }
        static void wait()
        {
            Console.Write("Чтобы продолжить, нажмите ENTER...");
            Console.ReadKey();
        }
    }
}