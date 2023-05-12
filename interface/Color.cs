using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Color
    {
        public static string MenuStr =
            "Меню связанное с цветами консоли\n" +
            "1. Фон - Серый, текст - Тёмно-синий (NEW)\n" +
            "2. Фон - Зелёный, текст - Белый (NEW)\n" +
            "3. Фон - Белый, текст - Чёрный (NEW)\n" +
            "4. Фон - Белый, текст - Тёмно-синий (NEW)\n" +
            "5. Фон - Тёмно-синий, текст - Белый (NEW)\n" +
            "6. Фон - Тёмно-зелёный, текст - Чёрный (NEW)\n" +
            "7. Фон - Тёмно-жёлтый, текст - Тёмно-синий (NEW)\n" +
            "8. Вернуть цвет консоли\n" +
            "0. Вернуться на главную";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Меню цвета");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Clear();
                        Console.WriteLine($"Вы успешно изменили тему консоли");
                        Title.Wait();
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.WriteLine($"Вы успешно изменили тему консоли");
                        Title.Wait();
                        break;
                    case 3:
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        Console.WriteLine($"Вы успешно изменили тему консоли");
                        Title.Wait();
                        break;
                    case 4:
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Clear();
                        Console.WriteLine($"Вы успешно изменили тему консоли");
                        Title.Wait();
                        break;
                    case 5:
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.WriteLine($"Вы успешно изменили тему консоли");
                        Title.Wait();
                        break;
                    case 6:
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        Console.WriteLine($"Вы успешно изменили тему консоли");
                        Title.Wait();
                        break;
                    case 7:
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Clear();
                        Console.WriteLine($"Вы успешно изменили тему консоли");
                        Title.Wait();
                        break;
                    case 8:
                        Console.ResetColor();
                        Console.Clear();
                        Console.WriteLine($"Вы изменили тему консоли на стандарт");
                        Title.Wait();
                        break;
                    case 0:
                        enableMenu = false;
                        break;
                    default:
                        enableMenu = false;
                        Console.Clear();
                        Console.WriteLine("Правильно введите номер пункта!");
                        Title.Set("Ошибка");
                        Title.Wait();
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Данные настройки были сохранены!");
            Title.Wait();
            return;
        }
    }
}