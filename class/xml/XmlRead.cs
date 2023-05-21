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
                Title.Set($"{Lang.GetText("title_page", currentPage, totalPages)}");
                string input;
                XmlNodeList FileNameNodes = document.SelectNodes($"/{MainTag}/{ObjectTag}[position()>={(currentPage -1) * pageSize + 1} and position()<={currentPage * pageSize}]");
                
                string skip = 
                    $"{Lang.GetText("string_skip_0")}\n" +
                    $"{Lang.GetText("string_skip_1")}\n" +
                    $"{Lang.GetText("string_skip_2")}";
                
                if (nodeList != null)
                    switch (FileName)
                    {
                        // Доступные файлы для работы данного метода:
                        case "doctors":
                            Console.WriteLine(Lang.GetText("line"));
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    $"{Lang.GetText("book_doctors_element_0", FileNameNode.SelectSingleNode("fullname-doctor")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_doctors_element_1", FileNameNode.SelectSingleNode("birthday")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_doctors_element_2", FileNameNode.SelectSingleNode("telegram-id")?.InnerText, FileNameNode.SelectSingleNode("id")?.InnerText)}\n" +
                                    $"{Lang.GetText("line")}"
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
                            Console.WriteLine($"{Lang.GetText("line")}");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    $"{Lang.GetText("book_cassa_element_0", FileNameNode.SelectSingleNode("id-admission")?.InnerText, FileNameNode.SelectSingleNode("id")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_cassa_element_1", FileNameNode.SelectSingleNode("amount")?.InnerText.ToString())}\n" +
                                    $"{Lang.GetText("book_cassa_element_2", FileNameNode.SelectSingleNode("date")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_cassa_element_3", FileNameNode.SelectSingleNode("status")?.InnerText)}\n" +
                                    $"{Lang.GetText("line")}"
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

                            Console.WriteLine($"{Lang.GetText("line")}");
                            foreach (XmlNode FileNameNode in FileNameNodes)
                            {
                                string doctorName = FileNameNode.SelectSingleNode("fullname-doctor")?.InnerText;
                                string id = FileNameNode.SelectSingleNode("id")?.InnerText;
                                XmlNode doctorNode = documentDoctors.SelectSingleNode($"./doctors/doctor[fullname-doctor='{doctorName}']");
                                XmlNode cassaNode = cassa.SelectSingleNode($"./cassa/payment[id-admission='{id}']");
                                Console.WriteLine($"{Lang.GetText("book_admission_element_0", FileNameNode.SelectSingleNode("date-time")?.InnerText, FileNameNode.SelectSingleNode("id")?.InnerText)}");

                                Console.Write($"{Lang.GetText("book_admission_element_1")}: ");
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
                                    doctorName += $" ({Lang.GetText("book_admission_element_1_variate")})";
                                }
                                
                                string payment = "";

                                if (cassaNode == null)
                                {
                                    payment = $"{Lang.GetText("book_admission_element_2")}";
                                }
                                else
                                {
                                    payment = $"{Lang.GetText("book_admission_element_2_variate", cassaNode.SelectSingleNode("amount")?.InnerText)}";
                                }

                                Console.Write($"{doctorName}");
                                Title.Theme(Title.activeTheme);
                                
                                Console.WriteLine("\n" +
                                    $"{Lang.GetText("book_admission_element_3", FileNameNode.SelectSingleNode("paciente-id")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_admission_element_4", FileNameNode.SelectSingleNode("complaints")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_admission_element_5", FileNameNode.SelectSingleNode("diagnosis")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_admission_element_6", FileNameNode.SelectSingleNode("info")?.InnerText)}\n" +
                                    $"{payment}\n" +
                                    $"{Lang.GetText("line")}"
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
                            Console.WriteLine($"{Lang.GetText("line")}");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    $"{Lang.GetText("book_pacientes_element_0", FileNameNode.SelectSingleNode("animal-type")?.InnerText, FileNameNode.SelectSingleNode("id")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_pacientes_element_1", FileNameNode.SelectSingleNode("name")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_pacientes_element_2", FileNameNode.SelectSingleNode("gender")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_pacientes_element_3", FileNameNode.SelectSingleNode("age")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_pacientes_element_4", FileNameNode.SelectSingleNode("fullname-owner")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_pacientes_element_5", FileNameNode.SelectSingleNode("valid")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_pacientes_element_6", FileNameNode.SelectSingleNode("telegram-id")?.InnerText)}\n" +
                                    $"{Lang.GetText("line")}"
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
                            Console.WriteLine($"{Lang.GetText("line")}");
                            foreach (XmlNode FileNameNode in FileNameNodes) 
                            {
                                Console.WriteLine(
                                    $"{Lang.GetText("book_statistic_element_1", FileNameNode.SelectSingleNode("date")?.InnerText)}\n" +
                                    $"{Lang.GetText("book_statistic_element_1", FileNameNode.SelectSingleNode("description")?.InnerText)}\n" +
                                    $"{Lang.GetText("line")}"
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
                            Title.Set($"{Lang.GetText("title_error_simpl")}");
                            Console.WriteLine($"{Lang.GetText("string_wrong_file")}");
                            Title.Wait();
                            break;
                    }
                else
                {
                    Title.Set($"{Lang.GetText("title_error_simpl")}");
                    Console.WriteLine($"{Lang.GetText("string_wrong_file")}");
                    Title.Wait();
                }
            }
        }
        // Показать информацию об одном элементе, указав ID
        public static void ShowById(string FileName, string MainTag, string ObjectTag, int id)
        {
            
            Title.Set($"{Lang.GetText("title_read_element", FileName)}");
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

                        Console.WriteLine($"{Lang.GetText("show_one_doctors_element_0")}:");
                        Console.WriteLine($"{Lang.GetText("show_one_doctors_element_1", fullName)}");
                        Console.WriteLine($"{Lang.GetText("show_one_doctors_element_2", birthday)}");
                        Console.WriteLine($"{Lang.GetText("show_one_doctors_element_3", telegramID, id)}");
                        break; 
                    case "admission":
                        string pacienteId = FileNameNode.SelectSingleNode("paciente-id").InnerText;
                        string dateTime = FileNameNode.SelectSingleNode("date-time").InnerText;
                        string fullnameDoctor = FileNameNode.SelectSingleNode("fullname-doctor").InnerText;
                        string complaints = FileNameNode.SelectSingleNode("complaints").InnerText;
                        string diagnosis = FileNameNode.SelectSingleNode("diagnosis").InnerText;
                        string info = FileNameNode.SelectSingleNode("info").InnerText;

                        Console.WriteLine($"{Lang.GetText("show_one_admission_element_0")}");
                        Console.WriteLine($"{Lang.GetText("show_one_admission_element_1", pacienteId, id)}");
                        Console.WriteLine($"{Lang.GetText("show_one_admission_element_2", dateTime)}");
                        Console.WriteLine($"{Lang.GetText("show_one_admission_element_3", fullnameDoctor)}");
                        Console.WriteLine($"{Lang.GetText("show_one_admission_element_4", complaints)}");
                        Console.WriteLine($"{Lang.GetText("show_one_admission_element_5", diagnosis)}");
                        Console.WriteLine($"{Lang.GetText("show_one_admission_element_6", info)}");
                        break;     
                    case "pacientes":
                        string animalType = FileNameNode.SelectSingleNode("animal-type").InnerText;
                        string name = FileNameNode.SelectSingleNode("name").InnerText;
                        string gender = FileNameNode.SelectSingleNode("gender").InnerText;
                        string age = FileNameNode.SelectSingleNode("age").InnerText;
                        string fullnameOwner = FileNameNode.SelectSingleNode("fullname-owner").InnerText;
                        string valid = FileNameNode.SelectSingleNode("valid").InnerText;
                        telegramID = FileNameNode.SelectSingleNode("telegram-id").InnerText;

                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_0")}:");
                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_1", animalType, id)}");
                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_2", name)}");
                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_3", gender)}");
                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_4", age)}");
                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_5", fullnameOwner)}");
                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_6", valid)}");
                        Console.WriteLine($"{Lang.GetText("show_one_pacientes_element_7", telegramID)}");
                        break;
                    default:
                        Console.WriteLine($"{Lang.GetText("title_error_input")}");
                        Title.Set(Lang.GetText("title_error_simpl"));
                        break;
                }
                Title.Wait();
            }
            else
            {
                Title.Set($"{Lang.GetText("title_error_search")}");
                Console.WriteLine($"{Lang.GetText("change_error_id")}");
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
        }
        
        public static void FindPacientByName()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./database/pacientes.xml");
            
            Console.Clear();
            Console.Write($"{Lang.GetText("find_pacient_input_name")}: ");
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
                    Console.WriteLine($"{Lang.GetText("find_pacient_element_0", count)}");
                    Console.WriteLine($"{Lang.GetText("find_pacient_element_1", idNode.InnerText)}");
                    Console.WriteLine($"{Lang.GetText("find_pacient_element_2", animalNode.InnerText)}");
                    Console.WriteLine($"{Lang.GetText("find_pacient_element_3", nameNode.InnerText)}");
                    Console.WriteLine($"{Lang.GetText("find_pacient_element_4", ownerNode != null ? ownerNode.InnerText : "Неизвестно")}");
                    Console.WriteLine(Lang.GetText("line"));

                    if (count > 1 && count % 2 == 0)
                    {
                        Title.Wait();
                        Console.ReadLine();
                    }
                }
            }

            if (count == 0)
            {
                Console.Clear();
                Title.Set($"{Lang.GetText("string_nothing_found")}");
                Console.WriteLine($"{Lang.GetText("string_nothing_found")}");
                Title.Wait();
            }
            else if (count == 1)
            {
                Title.Set($"{Lang.GetText("title_find_pacient_one_element")}");
                Console.WriteLine($"{Lang.GetText("find_pacient_one_element")}");
                Title.Wait();
            }
            else
            {
                Title.Set($"{Lang.GetText("title_find_pacient_more_element", count)}");
                Console.WriteLine($"{Lang.GetText("find_pacient_more_element", count)}");
                Title.Wait();
            }
        }
    }
}