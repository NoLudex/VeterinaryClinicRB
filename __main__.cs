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
        public static void Main(string[] args)
        {
            string mainTitle = "Ветеринарная клиника | "; 
            Console.Clear();
            // Проверка валидности ключа
            if (!ValidateAccessKey())
            { 
                Console.Title = mainTitle + "Верификация";
                Console.Clear();
                Console.Write("Введите ключ доступа к программе\nВвод: ");
                string ?key = Console.ReadLine();
                if (!ValidateAccessKey(key))
                {
                    Console.Clear();
                    Console.WriteLine("Неверный ключ доступа");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                    return;
                }
            }

            // Запуск цикла меню
            bool isRunning = true;

            try
            {
                while (isRunning)
                {
                    Console.Title = mainTitle + "Врачи";
                    Console.Clear();
                    Console.Write(
                        "Меню связанное с Врачами\n" +
                        "Выберите действие, введите номер действия\n" +
                        "1. Открыть список Врачей\n" +
                        "2. Показать врача, указав ID\n" +
                        "3. Изменить информацию врача по ID\n" +
                        "4. Добавить нового врача в список\n" +
                        "5. Удалить врача, указав ID\n" +
                        "6. Выйти в основное меню\n" +
                        "Ввод: "
                    );
    
                    int choice = int.Parse(Console.ReadLine());
    
                    switch (choice)
                    {
                        case 1:
                            ReadXML("doctors", "doctors", "doctor");
                            break;
                        case 2:
                            Console.Write("Введите ID: ");
                            int idToFind = int.Parse(Console.ReadLine());
                            ShowXMLByID("doctors", "doctors", "doctor", idToFind);
                            break;
                        case 3:
                            Console.Write("Введите ID: ");
                            int idToUpdate = int.Parse(Console.ReadLine());
                            UpdateXML("doctors", "doctors", "doctor", idToUpdate);
                            break;
                        case 4:
                            AddXML("doctors", "doctors", "doctor");
                            break;
                        case 5:
                            Console.Write("Введите ID: ");
                            int idToDelete = int.Parse(Console.ReadLine());
                            DeleteXML("doctors", "doctors", "doctor", idToDelete);
                            break;
                        case 6:
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Правильно введите номер действия");
                            break;
                    }
                    wait();
                }
            }
            catch (System.FormatException)
            {
                Console.Clear();
                Console.WriteLine("Вы указали неверный формат, запустите программу снова и повторите попытку");
                wait();
            }
        }
    }
}