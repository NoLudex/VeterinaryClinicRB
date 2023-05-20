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
                Console.Write($"{Lang.GetText("valid_input_name", NAME)}\n{Lang.GetText("string_input")}: ");
                string ?inputString = Console.ReadLine(); // NAME

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
                        Console.WriteLine($"[{Lang.GetText("string_attention")}] {Lang.GetText("valid_input_name_error0")}");
                    }
                }
                else // Ошибка
                {
                    Console.Clear();
                    Console.WriteLine($"[{Lang.GetText("string_attention")}] {Lang.GetText("valid_input_name_error1")}");
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
            Console.Write($"{Lang.GetText("valid_input_date", NAME)}\n{Lang.GetText("string_input")}: ");
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
                        Console.Write($"[{Lang.GetText("string_attention")}] {Lang.GetText("valid_input_date_error0")}\n{Lang.GetText("string_input")}:");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write($"[{Lang.GetText("string_attention")}] {Lang.GetText("valid_input_date_error1")}\n{Lang.GetText("string_input")}:");
                }
            }
            return inputDate;
        } 

        // Обработчик чисел в string значении!
        public static string Number(string TEXT)
        {
            double number = -1;
            while (number < 0)
            {
                Console.Clear();
                Console.Write(TEXT);
                string input = Console.ReadLine();
                if (double.TryParse(input, out number))
                    if (number >= 0)
                        return number.ToString();
                Console.Clear();
                Console.WriteLine($"{Lang.GetText("valid_input_number_error")}");
                Title.Wait();
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
                    Console.WriteLine($"[{Lang.GetText("error_input")}] {Lang.GetText("valid_input_tag_error")}");
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
            Console.WriteLine($"=-=-= {Lang.GetText("string_attention")} =-=-=");
            Console.WriteLine($"{Lang.GetText("valid_delete_data", NAME)}\n{Lang.GetText("string_input")}:");
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