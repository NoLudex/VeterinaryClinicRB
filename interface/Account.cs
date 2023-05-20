using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Account
    {
        public static string MenuStr =
            $"{Lang.GetText("Account_menu_1")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("Account_choice_1")}\n" +
            $"2. {Lang.GetText("Account_choice_2")}\n" +
            $"3. {Lang.GetText("Account_choice_3")}\n" +
            $"4. {Lang.GetText("Account_choice_4")}\n" +
            $"5. {Lang.GetText("Account_choice_5")}\n" +
            $"6. {Lang.GetText("Account_choice_6")}\n" +
            $"7. {Lang.GetText("Account_choice_7")}\n" +
            $"0. {Lang.GetText("Account_choice_0")}";
        public static void Menu()
        {
            bool enableMenu = true; 
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_account")}");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        Config.Set("Login", "");
                        Config.Set("AutoLogin", false);
                        Console.WriteLine($"{Lang.GetText("string_restart")}.");
                        Title.Wait();
                        Environment.Exit(0);
                        break;
                    case 6:
                        AccessKey.Menu(); // Включить меню настроек ключ-доступа
                        break;
                    case 7:
                        Lang.Menu(); // меню настроек языка
                        break;
                    case 0:
                        enableMenu = false;
                        break;
                    default:
                        enableMenu = false;
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("string_error_back")}");
                        Title.Set($"{Lang.GetText("title_error")}");
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}