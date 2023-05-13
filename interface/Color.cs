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
                        Title.ThemeSet(1);
                        break;
                    case 2:
                        Title.ThemeSet(2);
                        break;
                    case 3:
                        Title.ThemeSet(3);
                        break;
                    case 4:
                        Title.ThemeSet(4);
                        break;
                    case 5:
                        Title.ThemeSet(5);
                        break;
                    case 6:
                        Title.ThemeSet(6);
                        break;
                    case 7:
                        Title.ThemeSet(7);
                        break;
                    case 8:
                        Title.ThemeSet(8);
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
            return;
        }
    }
}