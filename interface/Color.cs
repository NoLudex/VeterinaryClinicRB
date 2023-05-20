using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Color
    {
        public static string MenuStr =
            $"{Lang.GetText("Меню связанное с цветами консоли")}\n" +
            $"1. {Lang.GetText("Colors_choice_1")}\n" +
            $"2. {Lang.GetText("Colors_choice_2")}\n" +
            $"3. {Lang.GetText("Colors_choice_3")}\n" +
            $"4. {Lang.GetText("Colors_choice_4")}\n" +
            $"5. {Lang.GetText("Colors_choice_5")}\n" +
            $"6. {Lang.GetText("Colors_choice_6")}\n" +
            $"7. {Lang.GetText("Colors_choice_7")}\n" +
            $"8. {Lang.GetText("Colors_choice_8")}\n" +
            $"0. {Lang.GetText("string_back_to_main_menu")}";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_colors")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

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
                        Console.WriteLine($"{Lang.GetText("string_error_input_choise")}");
                        Title.Set($"{Lang.GetText("title_error")}");
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}