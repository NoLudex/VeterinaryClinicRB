using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Valid
    {
        // Обычный обработчик на проверку вводимых данных ФИО пользователя
        public static string FullNameUser(string NAME)
        {   
            bool validInput = false;
            string lastName = "";
            string firstName = "";
            string middleName = "";

            Console.Clear();

            while (!validInput)
            {
                Console.Write($"Введите ФИО {NAME} на русском языке (Фамилия Имя отчество)\nВвод: ");
                string ?inputString = Console.ReadLine();

                // Разделяет текст на слова
                string[] inputArray = inputString.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);

                // Проверяем, что введены три слова
                if (inputArray.Length == 3)
                {
                    // Проверяем, что каждое слово написано на русском языке и без дополнительных знаков
                    if (IsRussianWithoutExtraSymbols(inputArray[0]) && IsRussianWithoutExtraSymbols(inputArray[1]) && IsRussianWithoutExtraSymbols(inputArray[2]))
                    {
                        lastName = inputArray[0];
                        firstName = inputArray[1];
                        middleName = inputArray[2];
                        validInput = true;
                    }
                    else // Ошибка
                    {
                        Console.Clear();
                        Console.WriteLine("[Внимание!] Неверный ввод. ФИО должно быть написано на русском языке и без доп. знаков, повторите ввод");
                    }
                }
                else // Ошибка
                {
                    Console.Clear();
                    Console.WriteLine("[Внимание!] Неверный ввод. Введите полное ФИО через пробелы");
                }
            }

            // Возвращает ФИО
            return $"{lastName} {firstName} {middleName}";
        }

        // Проверка, это русский текст без экстра-символов
        static bool IsRussianWithoutExtraSymbols(string text)
        {
            foreach (char c in text)
                if (!(Char.IsLetter(c) && "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".Contains(Char.ToLower(c))))
                    return false;
            return true;
        }
        
        // Обработчик ввода даты
        public static string Date(string NAME)
        {
            bool validInput = false;
            DateTime birthDate;
            string ?inputDate = "";
            
            Console.Clear();
            Console.Write($"Введите дату {NAME} в формате (ДД.ММ.ГГГГ)\nВвод: ");
            while (!validInput)
            {
                inputDate = Console.ReadLine();

                if (DateTime.TryParseExact(inputDate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate))
                {
                    if (birthDate <= DateTime.Now && birthDate.Year >= 1900)
                    {
                        validInput = true;
                    }
                    else
                    {   
                        Console.Clear();
                        Console.Write("[Внимание!] Неверная дата рождения. Дата должна быть не позже сегодняшнего дня и не раньше 01.01.1900\nВвод:");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("[Внимание!] Неверный формат даты. Формат должен быть (ДД.ММ.ГГГГ)\nВвод:");
                }
            }
            return inputDate;
        } 

        // Обработчик чисел в string значении!
        public static string Number(string TEXT)
        {
            Console.Clear();
            double number = -1;
            while (number < 0)
            {
                Console.Write(TEXT);
                string input = Console.ReadLine();
                if (double.TryParse(input, out number))
                    if (number >= 0)
                        return number.ToString();
                Console.Clear();
                Console.WriteLine("Неверный ввод. Нужно положительное число");
            }

            return "";
        }

        // Обработчик TelegramID
        public static string TelegramID(string TEXT)
        {
            bool validInput = false;
            string id = "";
            Console.Clear();
            while (!validInput)
            {
                Console.WriteLine(TEXT);
                string input = Console.ReadLine();

                // Проверяем, что сптрока на английской раскладке и не содержит недопустимых символов
                if (IsEnglishWithoutExtraSymbols(input))
                {
                    // Если ввод начинается с @, то добавляем в начало ID
                    if (input.StartsWith("@"))
                        id = input;
                    // Если ввод не начинается с @, то добавляем его в начало ID
                    else
                        id = "@" + input;
                    validInput = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неверный ввод. Тег должен быть на английской раскладке без спецсимволов (за исключением '-')");
                }
            }
            return id;
        }

        // Проверка текста, что написано на английском языке, без использования лишней символики
        static bool IsEnglishWithoutExtraSymbols(string TEXT)
        {
            foreach (char c in TEXT)
                if (!(Char.IsLetter(c) && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-".Contains(c)))
                    return false;
            return true;
        }

        public static bool Accept(string NAME)
        {
            Console.WriteLine("=-=-= Предупреждение! =-=-=");
            Console.WriteLine($"Данное действие удалит безвозвратно данные, вы согласны продолжить? ({NAME}) (да / нет <= (другой ответ))\nВвод:");
            string? choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice) || string.IsNullOrWhiteSpace(choice))
                return false;
            else if (choice.ToLower() == "да" || choice.ToLower() == "д" || choice.ToLower() == "y" || choice.ToLower() == "yes" )
                return true;
            else
                return false;
        }
    }
}