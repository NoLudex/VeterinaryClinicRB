using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Authorization
    {
        public static string MenuStr = 
            $"{Lang.GetText("Autorization_menu_0")}\n" +
            $"{Lang.GetText("Autorization_menu_1")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("Autorization_choice_1")}\n" +
            $"2. {Lang.GetText("Autorization_choice_2")}\n" +
            $"3. {Lang.GetText("Autorization_choice_3")}\n" +
            $"0. {Lang.GetText("string_exit")}";

        public static string nowLogin = Config.Get("Login");
        public static bool autoLogin = Config.Get("AutoLogin", true);
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_autorization")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1:
                        if (nowLogin == "Данный ключ отсутствует  ")
                            nowLogin = "";
                        string? fullname = Authorization.ValidateUser(autoLogin, login: nowLogin);
                        if (fullname != "")
                        {
                            while (true)
                                General.Menu(fullname);
                        }
                        break;
                    case 2:
                        Authorization.Register();
                        break;
                    case 3:
                        AccessKey.Menu();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("string_programm_will_closed")}");
                        Title.Wait();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("string_error_back")}");
                        Title.Set($"{Lang.GetText("title_error")}");
                        Title.Wait();
                        break;
                }
            }
        }
    }
}