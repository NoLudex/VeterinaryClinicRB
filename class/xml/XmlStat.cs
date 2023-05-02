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
                        string dayEvents = $"{date} ({eventsCount} событий):\n{events}\n";
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
                result = "На сегодня нет событий.";
            }

            return result;
        }

        public static void FindDoctorStats(string fullName)
        {
            Console.WriteLine($"Статистика приемов врача {fullName}:");

            // Загружаем базу данных врачей из файла
            XmlDocument doctorsDoc = new XmlDocument();
            doctorsDoc.Load("./database/doctors.xml");

            // Поиск врача в базе данных
            XmlNode doctorNode = doctorsDoc.SelectSingleNode($"doctors/doctor[fullname-doctor='{fullName}']");
            if (doctorNode == null)
            {
                Console.Clear();
                Title.Set("Результаты не найдены");
                Console.WriteLine("Данного врача нет в базе данных");
                Title.Wait();
                return;
            }

            // Загружаем базу данных приемов из файла
            XmlDocument admissionsDoc = new XmlDocument();
            admissionsDoc.Load("./database/admission.xml");

            // Фильтрация приемов по ФИО врача
            XmlNodeList admissionNodes = admissionsDoc.SelectNodes($"admissions/animal[fullname-doctor='{fullName}']");

            // Вывод результатов поиска
            if (admissionNodes.Count > 0)
            {
                foreach (XmlNode admissionNode in admissionNodes)
                {
                    Console.WriteLine($"Пациент: {admissionNode.SelectSingleNode("paciente-id").InnerText}");
                    Console.WriteLine($"Дата и время приема: {admissionNode.SelectSingleNode("date-time").InnerText}");
                    Console.WriteLine($"Жалобы: {admissionNode.SelectSingleNode("complaints").InnerText}");
                    Console.WriteLine($"Диагноз: {admissionNode.SelectSingleNode("diagnosis").InnerText}");
                    Console.WriteLine($"Рекомендации: {admissionNode.SelectSingleNode("info").InnerText}");
                    Console.WriteLine("----------------------------");
                }
                Title.Wait();
            }
            else
            {
                Console.Clear();
                Title.Set("Результаты не найдены");
                Console.WriteLine("У данного врача нет истории приёмов");
                Title.Wait();
            }
        }
    }
}