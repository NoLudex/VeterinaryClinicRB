using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Account
    {
        public static string MenuStr =
            $"{Lang.GetText("account_menu_1")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("account_choice_1")}\n" +
            $"2. {Lang.GetText("account_choice_2")}\n" +
            $"3. {Lang.GetText("account_choice_3")}\n" +
            $"4. {Lang.GetText("account_choice_4")}\n" +
            $"5. {Lang.GetText("account_choice_5")}\n" +
            $"6. {Lang.GetText("account_choice_6")}\n" +
            $"7. {Lang.GetText("account_choice_7")}\n" +
            $"0. {Lang.GetText("account_choice_0")}";
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
                        Account.PrintUserInfo(Authorization.nowLogin);
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write($"{Lang.GetText("account_input_login")}: ");
                        Account.PrintUserInfo(Console.ReadLine());
                        break;
                    case 3: 
                        Account.PrintUserLogins();
                        break;
                    case 4:
                        Account.ChangeUserData("./database/accounts.xml");
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