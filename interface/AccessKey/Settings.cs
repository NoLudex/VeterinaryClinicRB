using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class AccessKey
    {
        public static string MenuStr =
            $"{Lang.GetText("Access_menu_main")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("Access_choice_1")}\n" +
            $"2. {Lang.GetText("Access_choice_2")}\n" +
            $"3. {Lang.GetText("Access_choice_3")}\n" +
            $"0. {Lang.GetText("Access_choice_0")}";
        
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("title_acess_key");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1:
                        AccessKey.AutoCheckKey();
                        break;
                    case 2:
                        AccessKey.Delete();
                        break;
                    case 3:
                        AccessKey.Get();
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