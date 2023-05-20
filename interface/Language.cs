using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Lang
    {
        public static string MenuStr =
            "Меню связанное с изменением языка приложения\n" +
            "На данный момент вы используете РУССКИЙ язык.\n" + // На англ версии "РУССКИЙ" будет заменён на англ!
            "Если вы желаете изменить язык приложения, выберите номер языка ниже:\n" +
            "1. Русский язык (Используется)\n" + // Прикол в том, что на англ версии "используется" будет на втором пункте
            "2. Английский язык\n" +
            "0. Вернуться назад в меню";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Смена языка");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

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
                        Console.WriteLine("Неверный ввод. Возвращаем в предыдущее меню!");
                        Title.Set("Ошибка действия");
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