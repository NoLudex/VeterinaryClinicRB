using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace VeterinaryClinicRB
{
    class Program
    {
        public static string key = "";
        public static int j = 5;
        static void Main(string[] args)
        {
            // Получение ключа из аргументов командной строки
            if (args.Length > 0)
                key = args[0];
            else
            {
                Console.Clear();
                Title.Set("Верификация");
                Console.Write("Чтобы иметь доступ к программе, нужен специальный ключ. \nВы можете обратиться к администрации для получения данного ключа.\nВвод: ");
                key = Console.ReadLine();
                // key = "RBWEEXLOVE";
            }
            while (true)
            {
                // Проверка ключа доступа на соответстиве ключам
                if (AccessKey.CheckAccess(key))
                {
                    // Запуск цикла меню
                    try
                    {
                        bool globalMenu = true;
                        while (globalMenu)
                        {
                            Title.Set("Главная");
                            Console.Clear();
                            Console.WriteLine("Главное меню | VeterinaryClinicRB\nСделайте выбор, с каким меню работать:");
                            Console.Write(
                                "1. Врачи\n" +
                                "2. Приёмы\n" +
                                "3. Пациенты\n" +
                                "4. Статистика\n" +
                                "0. Выход из программы\n" +
                                "Ввод: "
                            );
            
                            int choiceMain;
                            string input = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(input))
                                break;
                            else
                                choiceMain = Convert.ToInt32(input);
            
                            switch (choiceMain)
                            {
                                case 1:
                                    bool doctorMenu = true;
                                    while (doctorMenu)
                                    {
                                        Title.Set("Меню врачи");
                                        Console.Clear();
                                        Console.Write(
                                            "Меню связанное с Врачами\n" +
                                            "Выберите действие из списка, которое желаете произвести\n" +
                                            "1. Открыть список Врачей\n" +
                                            "2. Показать Врача, указав ID\n" +
                                            "3. Изменить информацию Врача по ID\n" +
                                            "4. Добавить нового Врача в список\n" +
                                            "5. Удалить Врача, указав ID\n" +
                                            "6. Вывести статистику Врача по ФИО\n" +
                                            "0. Выйти в основное меню\n" +
                                            "Ввод: "
                                        );
    
                                        int choice;
                                        input = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(input))
                                            break;
                                        else
                                            choice = Convert.ToInt32(input);
                        
                                        switch (choice)
                                        {
                                            case 1:
                                                XmlRead.Book("doctors", "doctors", "doctor");
                                            break;
                                            case 2:
                                                Console.Write("Введите ID: ");
                                                int idToFind = int.Parse(Console.ReadLine());
                                                XmlRead.ShowById("doctors", "doctors", "doctor", idToFind);
                                            break;
                                            case 3:
                                                Console.Write("Введите ID: ");
                                                int idToUpdate = int.Parse(Console.ReadLine());
                                                XmlChange.Update("doctors", "doctors", "doctor", idToUpdate);
                                            break;
                                            case 4:
                                                XmlAdd.New("doctors", "doctors", "doctor");
                                            break;
                                            case 5:
                                                Console.Write("Введите ID: ");
                                                int idToDelete = int.Parse(Console.ReadLine());
                                                XmlDelete.This("doctors", "doctors", "doctor", idToDelete);
                                            break;
                                            case 6:
                                                string FindDoctor = Valid.FullNameUser("врача");
                                                XmlStat.FindDoctorStats(FindDoctor);
                                            break;
                                            case 228:
                                                XmlRead.PrintAdmissionData();
                                                Title.Wait();
                                            break;
                                            case 0:
                                                doctorMenu = false;
                                            break;
                                            default:
                                                Console.Clear();
                                                Console.WriteLine("Ошибка! Правильно введите номер действия");
                                                Title.Set("Ошибка");
                                                Title.Wait();
                                            break;
                                        }
                                    }
                                break;
                                case 2:
                                    bool admissionMenu = true;
                                    while (admissionMenu)
                                    {
                                        Title.Set("Меню приёмов");
                                        Console.Clear();
                                        Console.Write(
                                            "Меню связанное с Приёмами\n" +
                                            "Выберите действие из списка, которое желаете произвести\n" +
                                            "1. Открыть список Приёмов\n" +
                                            "2. Показать Приём, указав ID\n" +
                                            "3. Изменить информацию Приёма по ID\n" +
                                            "4. Добавить новый Приём в список\n" +
                                            "5. Удалить Приём, указав ID\n" +
                                            "6. Изменить информацию Приёма, указав ID\n" +
                                            "0. Выйти в основное меню\n" +
                                            "Ввод: "
                                        );
                        
                                        int choice;
                                        input = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(input))
                                            break;
                                        else
                                            choice = Convert.ToInt32(input);
                        
                                        switch (choice)
                                        {
                                            case 1:
                                                XmlRead.Book("admission", "admission", "animal");
                                            break;
                                            case 2:
                                                Console.Write("Введите ID: ");
                                                int idToFind = int.Parse(Console.ReadLine());
                                                XmlRead.ShowById("admission", "admission", "animal", idToFind);
                                            break;
                                            case 3:
                                                Console.Write("Введите ID: ");
                                                int idToUpdate = int.Parse(Console.ReadLine());
                                                XmlChange.Update("admission", "admission", "animal", idToUpdate);
                                            break;
                                            case 4:
                                                XmlAdd.New("admission", "admission", "animal");
                                            break;
                                            case 5:
                                                Console.Write("Введите ID: ");
                                                int idToDelete = int.Parse(Console.ReadLine());
                                                XmlDelete.This("admission", "admission", "animal", idToDelete);
                                            break;
                                            case 6:
                                                Console.Write("Введите ID: ");
                                                string idToChange = Console.ReadLine();
                                                XmlChange.UpdateInfo(idToChange);
                                            break;
                                            case 0:
                                                admissionMenu = false;
                                            break;
                                            default:
                                                Console.Clear();
                                                Console.WriteLine("Ошибка. Правильно введите номер действия!");
                                                Title.Set("Ошибка");
                                                Title.Wait();
                                            break;
                                        }
                                    }
                                break;
                                case 3:
                                    bool pacientesMenu = true;
                                    while (pacientesMenu)
                                    {
                                        Title.Set("Меню пациентов");
                                        Console.Clear();
                                        Console.Write(
                                            "Меню связанное с Пациентами\n" +
                                            "Выберите действие из списка, которое желаете произвести\n" +
                                            "1. Открыть список Пациентов\n" +
                                            "2. Показать Пациента, указав ID\n" +
                                            "3. Изменить информацию Пациента по ID\n" +
                                            "4. Добавить нового Пациента в список\n" +
                                            "5. Удалить Пациента, указав ID\n" +
                                            "6. Изменить валидность Пациента, указать ID\n" +
                                            "7. Найти Пациента(ов) по клички\n" +
                                            "0. Выйти в основное меню\n" +
                                            "Ввод: "
                                        );
                        
                                        int choice;
                                        input = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(input))
                                            break;
                                        else
                                            choice = Convert.ToInt32(input);
                        
                                        switch (choice)
                                        {
                                            case 1:
                                                XmlRead.Book("pacientes", "pacientes", "paciente");
                                            break;
                                            case 2:
                                                Console.Write("Введите ID: ");
                                                int idToFind = int.Parse(Console.ReadLine());
                                                XmlRead.ShowById("pacientes", "pacientes", "paciente", idToFind);
                                            break;
                                            case 3:
                                                Console.Write("Введите ID: ");
                                                int idToUpdate = int.Parse(Console.ReadLine());
                                                XmlChange.Update("pacientes", "pacientes", "paciente", idToUpdate);
                                            break;
                                            case 4:
                                                XmlAdd.New("pacientes", "pacientes", "paciente");
                                            break;
                                            case 5:
                                                Console.Write("Введите ID: ");
                                                int idToDelete = int.Parse(Console.ReadLine());
                                                XmlDelete.This("pacientes", "pacientes", "paciente", idToDelete);
                                            break;
                                            case 6:
                                                Console.Write("Введите ID: ");
                                                int idToChangeValid = int.Parse(Console.ReadLine());
                                                XmlDelete.This("pacientes", "pacientes", "paciente", idToChangeValid);
                                            break;
                                            case 7:
                                                XmlRead.FindPacientByName();
                                            break;
                                            case 0:
                                                pacientesMenu = false;
                                            break;
                                            default:
                                                Console.Clear();
                                                Console.WriteLine("Правильно введите номер действия");
                                                Title.Set("Ошибка");
                                                Title.Wait();
                                            break;
                                        }
                                    }
                                break;
                                case 4:
                                    bool statisticMenu = true;
                                    while (statisticMenu)
                                    {
                                        Title.Set("Меню статистики");
                                        Console.Clear();
                                        Console.Write(
                                            "Меню связанное со статистикой\n" +
                                            "Выберите действие из списка, которое желаете произвести\n" +
                                            "1. Открыть список/статистику по дням\n" +
                                            "2. Удалить статистику дня\n" +
                                            "3. Сделать сортировку статистики\n" +
                                            "4. Сегодняшняя статистика\n" +
                                            "0. Выйти в основное меню\n" +
                                            "Ввод: "
                                        );
                        
                                        int choice;
                                        input = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(input))
                                            break;
                                        else
                                            choice = Convert.ToInt32(input);
                        
                                        switch (choice)
                                        {
                                            case 1:
                                                XmlRead.Book("statistic", "statistic", "data");
                                            break;
                                            case 2:
                                                Console.Write("Введите дату (ДД.ММ.ГГГГ): ");
                                                string data = Console.ReadLine();
                                                if (string.IsNullOrWhiteSpace(data))
                                                    break;
                                                else
                                                    XmlDelete.DeleteStatistic(data);
                                            break;
                                            case 3:
                                                Console.Clear();
                                                Console.WriteLine("Производится сортировка...");
                                                XmlSort.SortByDate("./database/statistic.xml");
                                                Console.WriteLine("Успешно!");
                                                Title.Set("Успех!");
                                                Title.Wait();
                                            break;
                                            case 4:
                                                Console.Clear();
                                                XmlSort.SortByDate("./database/statistic.xml");
                                                Title.Set("Все события");
                                                Console.WriteLine(XmlStat.GetEventsToday());
                                                Title.Wait();
                                            break;
                                            case 0:
                                                statisticMenu = false;
                                            break;
                                            default:
                                                Console.Clear();
                                                Console.WriteLine("Правильно введите номер действия");
                                                Title.Set("Ошибка");
                                                Title.Wait();
                                            break;
                                        }
                                    }
                                break;
                                case 9:
                                    Console.Clear();
                                    Console.WriteLine("ПРЕДУПРЕЖДЕНИЕ!");
                                    Console.WriteLine("Изменение данных настроек может плохо отразиться на работоспособности приложения!\nЛюбые изменения производить на свой страх и риск!");
                                    bool DangerousSettings = true;
                                    while (DangerousSettings)
                                    {
                                        Title.Set("Настройки");
                                        Console.Clear();
                                        Console.Write(
                                            "ЭКСТРА НАСТРОЙКИ\n" +
                                            "Выберите меню из списка, которое желаете произвести\n" +
                                            "1. Изменение ID\n" +
                                            "2. Вкл/Выкл автоматической сортировки\n" +
                                            "Любой другой ввод => выход на главное меню\n" +
                                            "Ввод: "
                                        );
                        
                                        int choice;
                                        input = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(input))
                                            break;
                                        else
                                            choice = Convert.ToInt32(input);
                        
                                        switch (choice)
                                        {
                                            case 1:
                                                
                                                break;
                                            case 2:
            
                                                break;
                                            default:
                                                DangerousSettings = false;
                                                break;
                                        }
                                    }
                                break;
                                case 0:

                                break;
                                default:
                                    Console.WriteLine("Правильно введите номер действия");
                                    Title.Set("Ошибка");
                                    Title.Wait();
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Вывод сообщения об ошибке
                        Console.Clear();
                        Title.Set("Ошибка.");
                        Console.WriteLine("Возникла ошибка. Программа вернёт вас на Главную.");
                        Console.WriteLine("Чтобы не столкнуться с данной проблемой вновь, пишите в правильном формате!");
                        Console.WriteLine("[DEBUG]: " + ex.Message + "\n");
                        Title.Wait();
                    }
                }
                else
                {
                    Title.Set("Ошибка доступа!");
                    int i = j;
                    j += 5;
                    string dots = "";
                    int counter = 0;
                    while (i != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Неверный ключ доступа!");
                        Console.Write($"Повторите попытку через {i} секунд" + dots + "\n");
                        dots += ".";
                        if (++counter == 4)
                        {
                            dots = "";
                            counter = 0;
                        }
                        Thread.Sleep(550);
                        Console.Clear();
                        Console.WriteLine("Неверный ключ доступа!");
                        Console.Write($"Повторите попытку через {i} секунд" + dots + "\n");
                        dots += ".";
                        if (++counter == 4)
                        {
                            dots = "";
                            counter = 0;
                        }
                        Thread.Sleep(550);
                        i--;
                    }
                    Console.Clear();

                    Console.Clear();
                    Title.Set("Верификация");
                    Console.Write("Чтобы иметь доступ к программе, нужен специальный ключ. \nВы можете обратиться к администрации для получения данного ключа.\nВвод: ");
                    key = Console.ReadLine();
                }
            }
        }
    }
}