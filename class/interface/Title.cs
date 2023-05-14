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
            Console.Title = "Ветеринарная клиника | " + NAME;
        }

        // Обычный обработчик, который ждёт подтверждения
        public static void Wait()
        {
            Console.Write("Нажмите Enter, чтобы продолжить...");
            Console.ReadKey();
        }
        
        // Тема консоли. Проверка и установка значения
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
                Console.WriteLine("Вы успешно изменили цветовую тему консоли на #" + Number);
                Set("Тема #" + Number);
                Config.Set("ConsoleTheme", Number.ToString());
            }
            else if (Number == 0)
            {
                Console.WriteLine("Вы убрали цветовую тему консоли");
                Set("Тема убрана");
                Config.Set("ConsoleTheme", Number.ToString());
            }
            else
            {
                Console.WriteLine("Данная тема не обнаружена! Возвращаем на стандартную...");
                Set("Ошибка темы");
                Config.Set("ConsoleTheme", 0.ToString());
            }
            Title.Wait();
        }
    }
}