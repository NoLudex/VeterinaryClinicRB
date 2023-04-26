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
            Console.Clear();
            // Проверка валидности ключа
            if (!ValidateAccessKey())
            {
                Console.Clear();
                Console.Write("Введите ключ доступа программы.\nВвод: ");
                string ?key = Console.ReadLine();
                if (!ValidateAccessKey(key))
                {
                    Console.Clear();
                    Console.WriteLine("Неверный ключ доступа! Программа остановлена.");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                    return;
                }
            }

            // Запуск цикла меню
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.Write(
                    "Программа [VeterinaryClinicRB]\n" +
                    "Что вы желаете сделать?\n" +
                    "1. Показать всех врачей\n" +
                    "2. Показать врача, указав ID\n" +
                    "3. Обновить данные врача по ID\n" +
                    "4. Добавить нового врача\n" +
                    "5. Удалить врача, указав ID\n" +
                    "6. Выход с приложения\n" +
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
                        Console.WriteLine("Выберите правильный пункт.");
                        break;
                }
                wait();
            }
        }
    }
}