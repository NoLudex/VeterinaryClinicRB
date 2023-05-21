using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
using System.Globalization;

namespace VeterinaryClinicRB
{
    public class XmlStat
    {
        public static int GetEventsCountToday()
        {
            int count = 0;

            // Получаем сегодняшнюю дату
            string today = DateTime.Today.ToString("dd.MM.yyyy");

            // Загружаем базу данных из файла
            XDocument doc = XDocument.Load("./database/statistic.xml");

            // Проходимся по всем элементам базы данных
            foreach (XElement element in doc.Descendants("data"))
            {
                // Проверяем дату каждого элемента на совпадение с сегодняшней датой
                string date = (string)element.Element("date");
                if (date == today)
                {
                    // Если дата совпадает, увеличиваем счетчик событий на 1
                    count++;
                }
            }

            // Возвращаем количество событий
            return count;
        }
        public static string GetEventsToday()
        {
            // Получаем сегодняшнюю дату
            string today = DateTime.Today.ToString("dd.MM.yyyy");

            // Загружаем базу данных из файла
            XDocument doc = XDocument.Load("./database/statistic.xml");

            // Создаем словарь для хранения описаний событий по дням
            Dictionary<string, List<string>> eventsByDay = new Dictionary<string, List<string>>();

            // Проходимся по всем элементам базы данных
            int count = 1;
            foreach (XElement element in doc.Descendants("data"))
            {
                // Получаем дату и описание события
                string date = (string)element.Element("date");
                string description = (string)element.Element("description");

                if (date == today)
                {
                    // Если дата совпадает с сегодняшней датой,
                    // добавляем описание события в список событий на сегодня
                    if (!eventsByDay.ContainsKey(today))
                    {
                        eventsByDay.Add(today, new List<string>());
                    }
                    eventsByDay[today].Add($"{count++}. {description}");
                }
                else
                {
                    // Если дата не совпадает с сегодняшней датой,
                    // добавляем описание события в список событий этого дня
                    if (!eventsByDay.ContainsKey(date))
                    {
                        eventsByDay.Add(date, new List<string>());
                    }
                    eventsByDay[date].Add(description);
                }
            }

            // Создаем строку с описанием событий на сегодня
            string todayEvents = "";
            if (eventsByDay.ContainsKey(today))
            {
                todayEvents = $"\nСобытия на сегодня ({today}):" +
                    $"\n{string.Join("\n", eventsByDay[today])}\n";
            }

            // Создаем строку с описанием событий прошлых дней
            string pastEvents = "";
            foreach (string date in eventsByDay.Keys)
            {
                if (date != today)
                {
                    string events = string.Join("\n", eventsByDay[date]);
                    if (pastEvents.Length + events.Length <= 4000)
                    {
                        // Если описания событий не превышают лимит, добавляем их к строке с описанием прошлых дней
                        int eventsCount = eventsByDay[date].Count;
                        string dayEvents = $"{date} ({Lang.GetText("statistic_events_count", eventsCount)}):\n{events}\n";
                        pastEvents += dayEvents;
                    }
                    else
                    {
                        // Если описания событий превышают лимит, 
                        // добавляем "..." и выходим из цикла
                        pastEvents += "...\n";
                        break;
                    }
                }
            }

            // Создаем итоговую строку с описанием всех событий
            string result = todayEvents + pastEvents;
            if (string.IsNullOrEmpty(result))
            {
                result = $"{Lang.GetText("statistic_events_nothing")}";
            }

            return result;
        }

        public static void FindDoctorStats(string fullName)
        {
            // Загружаем базу данных врачей из файла
            XmlDocument doctorsDoc = new XmlDocument();
            doctorsDoc.Load("./database/doctors.xml");

            // Поиск врача в базе данных
            XmlNode doctorNode = doctorsDoc.SelectSingleNode($"doctors/doctor[fullname-doctor='{fullName}']");
            if (doctorNode == null)
            {
                Console.Clear();
                Title.Set(Lang.GetText("title_find_doctor_stat_nothing"));
                Console.WriteLine(Lang.GetText("find_doctor_stat_nothing"));
                Title.Wait();
                return;
            }

            // Загружаем базу данных приемов из файла
            XmlDocument admissionsDoc = new XmlDocument();
            admissionsDoc.Load("./database/admission.xml");

            // Фильтрация приемов по ФИО врача
            XmlNodeList admissionNodes = admissionsDoc.SelectNodes($"admission/animal[fullname-doctor='{fullName}']");

            // Вывод результатов поиска
            if (admissionNodes.Count > 0)
            {
                int i = 1;
                Console.Clear();
                foreach (XmlNode admissionNode in admissionNodes)
                {
                    Title.Set(Lang.GetText("line", i, admissionNodes.Count));
                    Console.WriteLine($"{Lang.GetText("find_doctor_stat_element_0", fullName)}:");
                    Console.WriteLine($"{Lang.GetText("find_doctor_stat_element_1", admissionNode.SelectSingleNode("paciente-id").InnerText)}");
                    Console.WriteLine($"{Lang.GetText("find_doctor_stat_element_2", admissionNode.SelectSingleNode("date-time").InnerText, admissionNode.SelectSingleNode("time").InnerText)}");
                    Console.WriteLine($"{Lang.GetText("find_doctor_stat_element_3", admissionNode.SelectSingleNode("complaints").InnerText)}");
                    Console.WriteLine($"{Lang.GetText("find_doctor_stat_element_4", admissionNode.SelectSingleNode("diagnosis").InnerText)}");
                    Console.WriteLine($"{Lang.GetText("find_doctor_stat_element_5", admissionNode.SelectSingleNode("info").InnerText)}");
                    Console.WriteLine(Lang.GetText("line"));
                    Title.Wait();
                }
            }
            else
            {
                Console.Clear();
                Title.Set($"{Lang.GetText("")}Результаты не найдены");
                Console.WriteLine($"{Lang.GetText("")}У данного врача нет истории приёмов");
                Title.Wait();
            }
        }

        public static void SearchByDate(string inputDate, int pageSize = 2)
        {
            XDocument admissionXml = XDocument.Load("./database/admission.xml");

            // Выполняем поиск по дате, используя LINQ to XML
            var result = from admission in admissionXml.Descendants("animal")
                        where admission.Element("date-time").Value == inputDate
                        select new
                        {
                            Id = admission.Element("id").Value,
                            Time = admission.Element("time").Value,
                            PacienteId = admission.Element("paciente-id").Value,
                            DateTime = admission.Element("date-time").Value,
                            FullNameDoctor = admission.Element("fullname-doctor").Value,
                            Complaints = admission.Element("complaints").Value,
                            Diagnosis = admission.Element("diagnosis").Value,
                            Info = admission.Element("info").Value
                        };

            int totalRecords = result.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            int currentPage = 1;

            while(true)
            {
                Console.Clear();
                
                // Получаем результаты по текущей странице
                var pagedResult = result.Skip((currentPage - 1) * pageSize).Take(pageSize);

                foreach (var admission in pagedResult)
                {
                    Console.WriteLine($"{Lang.GetText("")}ID: {admission.Id}");
                    Console.WriteLine($"{Lang.GetText("time_1", admission.Time)}");
                    Console.WriteLine($"{Lang.GetText("", admission.PacienteId)}");
                    Console.WriteLine($"{Lang.GetText("", admission.DateTime)}");
                    Console.WriteLine($"{Lang.GetText("", admission.FullNameDoctor)}");
                    Console.WriteLine($"{Lang.GetText("", admission.Complaints)}");
                    Console.WriteLine($"{Lang.GetText("", admission.Diagnosis)}");
                    Console.WriteLine($"{Lang.GetText("", admission.Info)}");
                    Console.WriteLine();
                }
                if (totalRecords == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"{Lang.GetText("no_admissions")}");
                    Title.Set($"{Lang.GetText("admissions_not_found")}");
                    Title.Wait();
                    return;
                }
                // Выводим информацию о количестве записей, текущей странице и общем количестве страниц
                Console.WriteLine($"{Lang.GetText("all_zapis", totalRecords, currentPage, totalPages)}");

                // Получаем команду от пользователя
                Console.Write($"{Lang.GetText("prev_next_1")}: ");
                string command = Console.ReadLine();

                // Обрабатываем команду пользователя
                if(command.ToLower() == "n")
                {
                    if(currentPage < totalPages)
                    {
                        currentPage++;
                    }
                }
                else if(command.ToLower() == "p")
                {
                    if(currentPage > 1)
                    {
                    currentPage--;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        public static void FindAdmissions(DateTime startTime, DateTime endTime)
        {
            XDocument document = XDocument.Load("./database/admission.xml");
            var admissions = document.Descendants("animal").Where(a =>
            {
                DateTime time = DateTime.ParseExact(a.Element("time").Value, "H:mm", null);
                DateTime date = DateTime.ParseExact(a.Element("date-time").Value, "dd.MM.yyyy", null);
                DateTime admissionTime = date + time.TimeOfDay;
                return admissionTime >= startTime && admissionTime <= endTime;
            }).ToList();
            int count = admissions.Count;

            if (count > 0)
            {
                Title.Set("Найдено приемов: " + count);

                int index = 0;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"{Lang.GetText("priem", index + 1)}:");
                    Console.WriteLine($"{Lang.GetText("time", admissions[index].Element("time").Value)}: ");
                    Console.WriteLine($"{Lang.GetText("id", admissions[index].Element("paciente-id").Value)}: ");
                    Console.WriteLine($"{Lang.GetText("data", admissions[index].Element("date-time").Value)}: ");
                    Console.WriteLine($"{Lang.GetText("FIO", admissions[index].Element("fullname-doctor").Value)}: ");
                    Console.WriteLine($"{Lang.GetText("con", admissions[index].Element("complaints").Value)}: " );
                    Console.WriteLine($"{Lang.GetText("diagnosis", admissions[index].Element("diagnosis").Value)}: "  );
                    Console.WriteLine($"{Lang.GetText("info", admissions[index].Element("info").Value)}: " );

                    Console.WriteLine($"\n{Lang.GetText("prev_next")}");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.N)
                    {
                        index++;
                        if (index == count) index = 0;
                    }
                    else if (keyInfo.Key == ConsoleKey.P)
                    {
                        index--;
                        if (index < 0) index = count - 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"{Lang.GetText("string_nothing_found")}");
            }
        }
        // Функция просмотра статистики по виду животного
        public static void GetStatsByAnimalType(string animalType)
        {
            List<Paciente> pacientes = Paciente.Get();
            List<Admission> admissions = Admission.Get();
            var filteredPacientes = pacientes.Where(p => string.Equals(p.AnimalType, animalType, StringComparison.OrdinalIgnoreCase)).ToList();
            int pageSize = 1; // размер страницы
            int pageCount = (int)Math.Ceiling((double)filteredPacientes.Count / pageSize);
            Console.Clear();
            int pageIndex = 0;
            if (pageCount == 0)
            {
                Console.Clear();
                Title.Set($"{Lang.GetText("title_error_search")}");
                Console.WriteLine($"{Lang.GetText("stats_error_animal_type")}");
                Title.Wait();
                return;
            }
            else
            {
                while (pageIndex < pageCount)
                {
                    Console.Clear();
                    Title.Set($"Категория: {animalType}");
                    Console.WriteLine($"{Lang.GetText("line")}");
                    Console.WriteLine($"{Lang.GetText("statistic_animal_element_0", animalType)}");
                    Console.WriteLine($"{Lang.GetText("title_page", pageIndex + 1, pageCount)}");
                    Console.WriteLine($"{Lang.GetText("statistic_animal_element_1", filteredPacientes.Count)}");
                    Console.WriteLine($"{Lang.GetText("line")}");

                    var pagePacientes = filteredPacientes.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    int admissionsCount = 0;

                    foreach (var paciente in pagePacientes)
                    {
                        Console.WriteLine($"{Lang.GetText("statistic_animal_element_2", paciente.Name, paciente.Gender, paciente.Age)}");

                        var pacienteAdmissions = admissions.Where(a => a.PacienteId == paciente.Id).ToList();
                        admissionsCount += pacienteAdmissions.Count;

                        if (pacienteAdmissions.Count > 0)
                        {
                            Console.WriteLine($"{Lang.GetText("general_choice_2")}:");
                            foreach (var admission in pacienteAdmissions)
                            {
                                Console.WriteLine(
                                    $"{Lang.GetText("statistic_animal_element_3", 
                                        admission.DateTime.ToString("dd.MM.yyyy H:mm"), 
                                        admission.FullnameDoctor, 
                                        admission.Complaints, 
                                        admission.Diagnosis, 
                                        admission.Info
                                    )}"
                                );
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{Lang.GetText("statistic_animal_element_nothing")}");
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine($"{Lang.GetText("statistic_animal_count", admissionsCount)}");

                    double avgAge = pagePacientes.Average(p => p.Age);
                    Console.WriteLine($"{Lang.GetText("statistic_animal_age", avgAge)}");

                    Console.WriteLine();
                    Console.WriteLine($"{Lang.GetText("statistic_anumal_skip")}");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        pageIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.Clear();
            System.Console.WriteLine($"{Lang.GetText("statistic_anumal_done", animalType)}");
            Title.Wait();
        }
    }
}