using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VeterinaryClinicRB
{
    public class Choice
    {
        // Ввод выбора номера меню и его проверка
        public static int Get()
        {
            // Ввод выбора
            string? choice = Console.ReadLine();

            // Проверка, есть ли что-то в данном вводе
            if (string.IsNullOrWhiteSpace(choice))
                return 0;
            else if (string.IsNullOrEmpty(choice))
                return 0;
            else if (Regex.IsMatch(choice, @"^\d+$"))
                return Convert.ToInt32(choice);
            else
                return 0;
        }
    }
}