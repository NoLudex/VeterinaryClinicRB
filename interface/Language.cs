using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Lang
    {
        public static string MenuStr =
            $"{Lang.GetText("Language_menu_0")}Меню связанное с изменением языка приложения\n" +
            $"{Lang.GetText("Language_menu_1")}На данный момент вы используете РУССКИЙ язык.\n" + // На англ версии "РУССКИЙ" будет заменён на англ!
            $"{Lang.GetText("Language_menu_2")}Если вы желаете изменить язык приложения, выберите номер языка ниже:\n" +
            $"1.{Lang.GetText("Language_choice_1")}\n" + // Прикол в том, что на англ версии "используется" будет на втором пункте
            $"2.{Lang.GetText("Language_choice_2")}\n" +
            $"0.{Lang.GetText("string_back_to_main_menu")}";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_language")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1:
                        Change("ru");
                        break;
                    case 2:
                        Change("en");
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
        }
        public static void Welcome()
        {
            bool welcomeEnable = Config.Get("Welcome", true);
            if (welcomeEnable)
            {
                Console.Clear();
                Title.Set("Welcome!");
                Console.Write( // Всё, что ниже, переводу не подлежит!!! (Для Ильи)
                    "[EN] Set the language of the program, make a choice of the language that is available from the list\n" +
                    "[RU] Установите язык программы, сделайте выбор языка, который доступен из списка\n" +
                    "1. ru - Russian [Руссский]\n" +
                    "2. en - English [Английский] (Default [По умолчанию])\n" +
                    "Set lang: "
                    );
      
                string? choice = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.Clear();
                    Console.WriteLine("Lang set to ENGLISH");
                    choice = "en";
                }
                else if (choice == "1" || choice.ToLower() == "ru" || choice.ToLower() == "Russian" || choice.ToLower() == "ру" || choice.ToLower() == "русский" || choice.ToLower() == "россия")
                {
                    Console.Clear();
                    Console.WriteLine("Язык установлен на русский. Чтобы продолжить, требуется перезапустить программу!");
                    Config.Set("Lang", "ru");
                    Config.Set("Welcome", false);
                    Title.Wait();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Lang set to ENGLISH");
                }
                Config.Set("Welcome", false);
            }
            return;
        }
    }
}