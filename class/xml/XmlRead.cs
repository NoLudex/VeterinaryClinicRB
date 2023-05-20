using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
using System.Threading.Tasks;
using System.Text;

namespace VeterinaryClinicRB
{
    public class XmlRead
    {
        // Вывод данных в список:
        public static void Book(string FileName, string MainTag, string ObjectTag)
        {
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            XmlNodeList ?nodeList = document.SelectNodes($"/{MainTag}/{ObjectTag}");
            int currentPage = 1; // Текущая страница
            int pageSize = 2; // Количество записей на странице
            if (FileName == "statistic" || FileName == "admission")
                pageSize = 1;
            if (FileName == "cassa")
                pageSize = 3;
            int totalPages = (int)Math.Ceiling((double)document.SelectNodes($"/{MainTag}/{ObjectTag}").Count / pageSize); // Общее количество страниц
            
            bool finished = false;

            while (!finished)
            {
                Console.Clear();
                Title.Set($"Страница {currentPage} из {totalPages}"); // Используй две переменные %1$ и %2$, через запятую указывай две переменных, удали этот комментарий потом!
                string input;
                XmlNodeList FileNameNodes = document.SelectNodes($"/{MainTag}/{ObjectTag}[position()>={(currentPage -1) * pageSize + 1} and position()<={currentPage * pageSize}]");
                
                string skip = 
                    "Чтобы посмотреть следующую страницу введите 'n'\n" +
                    "Введите 'p' Чтобы просмотреть предыдущую страницу, \n" +
                    "Любая другая клавиша выводит вас с данного меню";
                
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
                                    "Пациентов прошло данного врача:  " + FileNameNode.SelectSingleNode("animals-treated")?.InnerText + "\n" +
                                    "Telegram ID:  " + FileNameNode.SelectSingleNode("telegram-id")?.InnerText + " (" + FileNameNode.SelectSingleNode("id")?.InnerText + ")\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine(skip);
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
                        case "cassa":
                            Console.WriteLine("-----------------------------");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    $"Идентификатор приёма: {FileNameNode.SelectSingleNode("id-admission")?.InnerText} (Лот {FileNameNode.SelectSingleNode("id")?.InnerText})\n" +
                                    "Стоимость приёма: " + FileNameNode.SelectSingleNode("amount")?.InnerText + "\n" +
                                    "Дата:  " + FileNameNode.SelectSingleNode("date")?.InnerText + "\n" +
                                    "Статус:  " + FileNameNode.SelectSingleNode("status")?.InnerText + "\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine(skip);
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
                            Title.Theme(Title.activeTheme);

                            XmlDocument documentDoctors = new XmlDocument();
                            documentDoctors.Load($"./database/doctors.xml");

                            XmlDocument cassa = new XmlDocument();
                            cassa.Load($"./database/cassa.xml");

                            Console.WriteLine("-----------------------------");
                            foreach (XmlNode FileNameNode in FileNameNodes)
                            {
                                string doctorName = FileNameNode.SelectSingleNode("fullname-doctor")?.InnerText;
                                string id = FileNameNode.SelectSingleNode("id")?.InnerText;
                                XmlNode doctorNode = documentDoctors.SelectSingleNode($"./doctors/doctor[fullname-doctor='{doctorName}']");
                                XmlNode cassaNode = cassa.SelectSingleNode($"./cassa/payment[id-admission='{id}']");
                                Console.WriteLine($"Дата: {FileNameNode.SelectSingleNode("date-time")?.InnerText} ID ({FileNameNode.SelectSingleNode("id")?.InnerText})");

                                Console.WriteLine("ФИО доктора: ");
                                // Проверяем наличие доктора в базе данных
                                if (doctorNode != null)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    doctorName += " (уволен)";
                                }
                                
                                string payment = "";

                                if (cassaNode == null)
                                {
                                    payment = "Приём бесплатный";
                                }
                                else
                                {
                                    payment = $"Приём платный, его стоимость: {cassaNode.SelectSingleNode("amount")?.InnerText}";
                                }

                                Console.Write($"{doctorName}");
                                Title.Theme(Title.activeTheme);
                                
                                Console.WriteLine("\n" +
                                    $"Идентификатор пациента: ID ({FileNameNode.SelectSingleNode("paciente-id")?.InnerText})\n" +
                                    $"Жалобы: {FileNameNode.SelectSingleNode("complaints")?.InnerText}\n" +
                                    $"Диагноз: {FileNameNode.SelectSingleNode("diagnosis")?.InnerText}\n" +
                                    $"Рекомендации от доктора: {FileNameNode.SelectSingleNode("info")?.InnerText}\n" +
                                    $"{payment}\n" +
                                    "-----------------------------"
                                );
                            }
                            Console.WriteLine(skip);
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
                                    "Вид животного: " + FileNameNode.SelectSingleNode("animal-type")?.InnerText + " ID (" + FileNameNode.SelectSingleNode("id")?.InnerText + ")\n" +
                                    "Кличка: " + FileNameNode.SelectSingleNode("name")?.InnerText + "\n" +
                                    "Пол: " + FileNameNode.SelectSingleNode("gender")?.InnerText + "\n" +
                                    "Возраст животного:  " + FileNameNode.SelectSingleNode("age")?.InnerText + "\n" +
                                    "ФИО владельца:  " + FileNameNode.SelectSingleNode("fullname-owner")?.InnerText + "\n" +
                                    "Валидно ли животное (?):  " + FileNameNode.SelectSingleNode("valid")?.InnerText + "\n" +
                                    "Телеграм владельца: " + FileNameNode.SelectSingleNode("telegram-id")?.InnerText + "\n" +
                                    "-----------------------------"
                                    );
                            }
                            Console.WriteLine(skip);
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
                            Console.WriteLine(skip);
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
                            Title.Set($"Ошибка!");
                            Console.WriteLine("[DEBUG] Неверно указан файл");
                            Title.Wait();
                            break;
                    }
                else
                {
                    Title.Set($"Ошибка!");
                    Console.WriteLine("[DEBUG] Ошибка чтения");
                    Title.Wait();
                }
            }
        }

        // Показать информацию об одном элементе, указав ID
        public static void ShowById(string FileName, string MainTag, string ObjectTag, int id)
        {
            
            Title.Set($"Просмотр элемента в {FileName}.xml");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");

            // Поиск и просмотр элемента в БД по ID
            XmlNode FileNameNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}[id='" + id + "']");
            Console.Clear();
            if (FileNameNode != null)
            {
                string telegramID = "";
                switch (FileName)
                {
                    case "doctors":
                        string fullName = FileNameNode.SelectSingleNode("fullname-doctor").InnerText;
                        string birthday = FileNameNode.SelectSingleNode("birthday").InnerText;
                        string animalsTreated = FileNameNode.SelectSingleNode("animals-treated").InnerText;
                        telegramID = FileNameNode.SelectSingleNode("telegram-id").InnerText;

                        Console.WriteLine("Информация об враче:");
                        Console.WriteLine($"ФИО врача: {fullName}");
                        Console.WriteLine("Дата рождения: " + birthday);
                        Console.WriteLine("Пациентов прошло данного врача: " + animalsTreated);
                        Console.WriteLine("Telegram ID: " + telegramID + $" ({id})");
                        break; 
                    case "admission":
                        string pacienteId = FileNameNode.SelectSingleNode("paciente-id").InnerText;
                        string dateTime = FileNameNode.SelectSingleNode("date-time").InnerText;
                        string fullnameDoctor = FileNameNode.SelectSingleNode("fullname-doctor").InnerText;
                        string complaints = FileNameNode.SelectSingleNode("complaints").InnerText;
                        string diagnosis = FileNameNode.SelectSingleNode("diagnosis").InnerText;
                        string info = FileNameNode.SelectSingleNode("info").InnerText;

                        Console.WriteLine("Информация об приёме:");
                        Console.WriteLine($"ID пациента: {pacienteId}; ID приёма: ({id})");
                        Console.WriteLine("Дата приёма: " + dateTime);
                        Console.WriteLine("ФИО врача: " + fullnameDoctor);
                        Console.WriteLine("Жалобы пациента: " + complaints);
                        Console.WriteLine("Диагноз: " + diagnosis);
                        Console.WriteLine($"Рекомендации от доктора: {info}");
                        break;     
                    case "pacientes":
                        string animalType = FileNameNode.SelectSingleNode("animal-type").InnerText;
                        string name = FileNameNode.SelectSingleNode("name").InnerText;
                        string gender = FileNameNode.SelectSingleNode("gender").InnerText;
                        string age = FileNameNode.SelectSingleNode("age").InnerText;
                        string fullnameOwner = FileNameNode.SelectSingleNode("fullname-owner").InnerText;
                        string valid = FileNameNode.SelectSingleNode("valid").InnerText;
                        telegramID = FileNameNode.SelectSingleNode("telegram-id").InnerText;

                        Console.WriteLine("Информация об пациенте:");
                        Console.WriteLine($"Вид животного: {animalType}; ID ({id})");
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
                        break;
                }
                Title.Wait();
            }
            else
            {
                Title.Set("Ошибка поиска");
                Console.WriteLine("Ничего не найдено. Проверьте, верно ли вы указали ID");
                Title.Wait();
            }
        }
        // Получить Максимальный ID из файла конфигурации
        public static int GetFreeID(string FileName)
        {
            XDocument doc = XDocument.Load($"./database/{FileName}.xml");
            int ID = 1;

            while (true)
            {
                bool idExists = doc.Descendants("id").Any(x => (int)x == ID);
                if (!idExists)
                    return ID;
                ID++;
            }
            // Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // int newId = Convert.ToInt32(config.AppSettings.Settings[$"Max{FileName}Id"].Value);
            // config.AppSettings.Settings[$"Max{FileName}Id"].Value = $"{newId + 1}";
            // config.Save();
            // return newId;
        }
        
        public static void FindPacientByName()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./database/pacientes.xml");
            
            Console.Clear();
            Console.Write("Введите критерий поиска (кличка животного): ");
            string searchCriteria = Console.ReadLine();
            Console.Clear();
            XmlNodeList pacientesList = xmlDoc.GetElementsByTagName("paciente");
            int count = 0;

            foreach (XmlNode pacienteNode in pacientesList)
            {
                XmlNode idNode = pacienteNode.SelectSingleNode("id");
                XmlNode animalNode = pacienteNode.SelectSingleNode("animal-type");
                XmlNode nameNode = pacienteNode.SelectSingleNode("name");
                XmlNode ownerNode = pacienteNode.SelectSingleNode("fullname-owner");

                if (nameNode != null && nameNode.InnerText.ToLower().Contains(searchCriteria.ToLower()))
                {
                    count++;
                    Console.WriteLine("Найден пациент №{0}:", count);
                    Console.WriteLine("  ID: {0}", idNode.InnerText);
                    Console.WriteLine("  Вид: {0}", animalNode.InnerText);
                    Console.WriteLine("  Кличка: {0}", nameNode.InnerText);
                    Console.WriteLine("  Владелец: {0}", ownerNode != null ? ownerNode.InnerText : "Неизвестно");
                    Console.WriteLine("-----------------");

                    if (count > 1 && count % 2 == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Нажмите Enter, чтобы продолжить...");
                        Console.ReadLine();
                    }
                }
            }

            if (count == 0)
            {
                Console.Clear();
                Title.Set("Результаты не найдены");
                Console.WriteLine("Результаты не найдены.");
                Title.Wait();
            }
            else if (count == 1)
            {
                Title.Set("Найден один пациент");
                Console.WriteLine("Найден единственный пациент.");
                Title.Wait();
            }
            else
            {
                Title.Set($"Найдено {count}");
                Console.WriteLine("Найдено {0} пациентов.", count);
                Title.Wait();
            }
        }

        public static void PrintAdmissionData(string date = null, string startDate = null, string endDate = null)
        {
            // Загрузка баз данных XML в объекты XmlDocument
            var admissionDoc = new XmlDocument();
            admissionDoc.Load("./database/admission.xml");

            var doctorsDoc = new XmlDocument();
            doctorsDoc.Load("./database/doctors.xml");

            var pacientesDoc = new XmlDocument();
            pacientesDoc.Load("./database/pacientes.xml");

            var statisticDoc = new XmlDocument();
            statisticDoc.Load("./database/statistic.xml");

            var admissionNodes = admissionDoc.SelectNodes("//admission");
            var doctorsNodes = doctorsDoc.SelectNodes("//doctor");
            var pacientesNodes = pacientesDoc.SelectNodes("//paciente");
            var statisticNodes = statisticDoc.SelectNodes("//data");

            int totalCount = 0;

            // Проходим по всем записям приема в заданном диапазоне дат
            foreach (XmlNode admissionNode in admissionNodes)
            {
                var dateTimeString = admissionNode.SelectSingleNode("date-time").InnerText;
                var admissionDate = DateTime.ParseExact(dateTimeString, "dd.MM.yyyy", null);

                if (date != null && admissionDate.ToString("dd.MM.yyyy") != date)
                {
                    continue;
                }

                if (startDate != null)
                {
                    var startDateObj = DateTime.ParseExact(startDate, "dd.MM.yyyy", null);
                    if (admissionDate < startDateObj)
                    {
                        continue;
                    }
                }

                if (endDate != null)
                {
                    var endDateObj = DateTime.ParseExact(endDate, "dd.MM.yyyy", null);
                    if (admissionDate > endDateObj)
                    {
                        continue;
                    }
                }

                // Получение информации о животном, враче и пациенте
                var animalId = admissionNode.SelectSingleNode("paciente-id").InnerText;

                // Перебираем все элементы пациентов для получения информации о требуемом животном
                foreach (XmlNode pacienteNode in pacientesNodes)
                {
                    var pacienteId = pacienteNode.SelectSingleNode("id").InnerText;

                    if (pacienteId == animalId)
                    {
                        var animalType = pacienteNode.SelectSingleNode("animal-type").InnerText;
                        var animalName = pacienteNode.SelectSingleNode("name").InnerText;
                        var animalGender = pacienteNode.SelectSingleNode("gender").InnerText;
                        var animalAge = pacienteNode.SelectSingleNode("age").InnerText;
                        var ownerFullName = pacienteNode.SelectSingleNode("fullname-owner").InnerText;

                        var doctorFullName = admissionNode.SelectSingleNode("fullname-doctor").InnerText;

                        // Перебираем все элементы врачей для получения информации о требуемом враче
                        foreach (XmlNode doctorNode in doctorsNodes)
                        {
                            var doctorFullNameInXml = doctorNode.SelectSingleNode("fullname-doctor").InnerText;

                            if (doctorFullNameInXml == doctorFullName)
                            {
                                var doctorSpecialization = doctorNode.SelectSingleNode("doctor-specialization").InnerText;

                                var complaints = admissionNode.SelectSingleNode("complaints").InnerText;
                                var diagnosis = admissionNode.SelectSingleNode("diagnosis").InnerText;
                                var info = admissionNode.SelectSingleNode("info").InnerText;
                                var admissionTime = admissionNode.SelectSingleNode("time").InnerText;

                                // Вывод информации в заданном формате
                                Console.WriteLine($"{admissionTime}, врач: {doctorFullName} ({doctorSpecialization}), пациент: {animalType} ({animalName}) [{animalGender}], возраст: {animalAge} лет, Владелец: {ownerFullName}, жалобы: {complaints}, диагноз: {diagnosis}, инфо: {info}.");

                                totalCount++;
                                break;
                            }
                        }

                        break;
                    }
                }
            }

            // Вывод общего количества приемов
            Console.WriteLine($"Всего приемов за день: {totalCount}");

            // Разделительная линия
            Console.WriteLine("============================");
        }
    }
}