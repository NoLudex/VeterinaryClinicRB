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
        static void Main(string[] args)
        {
            string key = "";

            // Получение ключа из аргументов командной строки
            if (args.Length > 0)
                key = args[0];
            else
            {
                Console.Clear();
                Title.Set("Верификация");
                Console.Write("Чтобы иметь доступ к программе, нужен специальный ключ. \nВы можете обратиться к администрации для получения данного ключа.\nВвод: ");
                key = Console.ReadLine();
            }

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
                        Console.WriteLine("Главное меню | VeterinaryClinicRB\nВыберите из списка файл, с которым желаете работать:");
                        Console.Write(
                            "1. doctors.xml (Врачи)\n" +
                            "2. admission.xml (Приёмы)\n" +
                            "3. pacientes.xml (Пациенты)\n" +
                            "4. statistic.xml (Статистика)\n" +
                            "Ввод: "
                        );
        
                        int choiceMain = Convert.ToInt32(Console.ReadLine());
        
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
                                        "0. Выйти в основное меню\n" +
                                        "Ввод: "
                                    );
        
                                    int choice = int.Parse(Console.ReadLine());
                    
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
                                        case 0:
                                            doctorMenu = false;
                                            break;
                                        default:
                                            doctorMenu = false;
                                            Console.WriteLine("Правильно введите номер действия");
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
                                        "7. Изменить валидность Приёма, указать ID\n" +
                                        "0. Выйти в основное меню\n" +
                                        "Ввод: "
                                    );
                    
                                    int choice = int.Parse(Console.ReadLine());
                    
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
                                        case 0:
                                            admissionMenu = false;
                                            break;
                                        default:
                                            admissionMenu = false;
                                            Console.WriteLine("Правильно введите номер действия");
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
                                        "0. Выйти в основное меню\n" +
                                        "Ввод: "
                                    );
                    
                                    int choice = int.Parse(Console.ReadLine());
                    
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
                                        case 0:
                                            pacientesMenu = false;
                                            break;
                                        default:
                                            pacientesMenu = false;
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
                                        "2. Изменить статистику дня\n" +
                                        "3. Сделать сортировку статистики\n" +
                                        "0. Выйти в основное меню\n" +
                                        "Ввод: "
                                    );
                    
                                    int choice = int.Parse(Console.ReadLine());
                    
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
                                            Console.WriteLine("Производится сортировка...");
                                            XmlSort.SortByDate("./././database/statistic.xml");
                                            Console.WriteLine("Успешно!");
                                            Title.Set("Успех!");
                                            Title.Wait();
                                            break;
                                        case 0:
                                            statisticMenu = false;
                                            break;
                                        default:
                                            statisticMenu = false;
                                            Console.WriteLine("Правильно введите номер действия");
                                            Title.Set("Ошибка");
                                            Title.Wait();
                                            break;
                                    }
                                }
                            break;
                            case 5:
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
                    
                                    int choice = int.Parse(Console.ReadLine());
                    
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
                    Console.WriteLine("Возникла ошибка. Программа будет перезапущена.");
                    Console.WriteLine("Чтобы не столкнуться с данной проблемой вновь, пишите в правильном формате!");
                    Console.WriteLine("Ошибка: " + ex.Message + "\n");
                    Title.Wait();

                    // Перезапуск программы с передачей ключа
                    Process.Start(Assembly.GetExecutingAssembly().Location, key);
                    
                    // Завершение текущего экземпляра программы
                    return;
                }
            }
            else
            {
                Console.Clear();
                Title.Set("Ошибка доступа!");
                Console.WriteLine("Неверный ключ доступа!");
                Console.WriteLine("Доступ запрещен! Программа закроется через 5 секунд...");
                Thread.Sleep(5000);
            }
        }
    }
}