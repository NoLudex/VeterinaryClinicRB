using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace VeterinaryClinicRB
{
    public class Title
    {
        // Выставление названия консоли с именем {NAME}
        public static void Set(string NAME)
        {
            Console.Title = Lang.GetText("title_main") + " | " + NAME;
        }

        // Обычный обработчик, который ждёт подтверждения
        public static void Wait()
        {
            Console.Write($"{Lang.GetText("enter_for_done")} ");
            Console.ReadKey();
        }
        
        // Тема консоли. Проверка и установка значения

        public static int activeTheme = Convert.ToInt32(Config.Get("ConsoleTheme"));
        public static void Theme(int Number)
        {
            switch (Number)
            {
                case 1:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 4:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 5:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 6: 
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 7:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 0:
                    Console.ResetColor();
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }

        public static void ThemeSet(int Number)
        {
            Theme(Number);
            Console.Clear();
            if (Number > 0 && Number <= 7)
            {
                Console.WriteLine(Lang.GetText("console_color_set", Number));
                Set(Lang.GetText("title_console_color_set", Number));
                Config.Set("ConsoleTheme", Number.ToString());
                activeTheme = Number;
            }
            else if (Number == 0)
            {
                Console.WriteLine(Lang.GetText("console_color_clear"));
                Title.Set(Lang.GetText("title_console_color_clear"));
                Config.Set("ConsoleTheme", Number.ToString());
                activeTheme = 0;
            }
            else
            {
                Console.WriteLine(Lang.GetText("console_color_error"));
                Set(Lang.GetText("title_console_color_error"));
                Config.Set("ConsoleTheme", 0.ToString());
                activeTheme = 0;
            }
            Title.Wait();
        }
    }
}