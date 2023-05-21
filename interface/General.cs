using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class General
    {
        public static string MenuStr0 =
            "Главное меню | VeterinaryClinicRB\n" +
            "Сегодня было совершено";
        public static string MenuStr1 =
            $"{Lang.GetText("General_menu_0")}:\n" +
            $"1. {Lang.GetText("General_choice_1")}\n" +
            $"2. {Lang.GetText("General_choice_2")}\n" +
            $"3. {Lang.GetText("General_choice_3")}\n" +
            $"4. {Lang.GetText("General_choice_4")}\n" +
            $"5. {Lang.GetText("General_choice_5")}\n" +
            $"6. {Lang.GetText("General_choice_6")}\n" +
            $"0. {Lang.GetText("string_exit")}";
        public static void Menu(string fullnameUser)
        {
            try
            {
                bool enableMenu = true;
                while (enableMenu)
                {
                    Statistic statistic = new Statistic("./database/statistic.xml");
                    int todayCount = statistic.GetTodayCount();
                    
                    Title.Set($"{Lang.GetText("title_general")}");
                    Console.Clear();
                    Console.Write($"{Lang.GetText("General_menu_1", MenuStr0, todayCount)}\n{Lang.GetText("General_menu_2", Authorization.nowLogin)}\n{MenuStr1}\n{Lang.GetText("string_input")}: ");

                    switch (Choice.Get())
                    {
                        case 1:
                            Doctor.Menu(); // Меню врача (Doctor.cs)
                            break;
                        case 2:
                            Admission.Menu(); // Меню приёмов (Admission.cs)
                            break;
                        case 3:
                            Pacientes.Menu(); // Меню пациентов (Pacientes.cs)
                            break;
                        case 4:
                            Cassa.Menu(); // Меню касса (Cassa.cs)
                            break;
                        case 5:
                            Statistic.Menu(); // Меню статистики (./Statistic/Main.cs)
                            break;
                        case 6:
                            Account.Menu(); // Меню аккаунта (Account.cs)
                            break;
                        case 7:
                            Color.Menu(); // Меню цветовой схемы (Color.cs)
                            break;
                        case 0:
                            enableMenu = false;
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("string_error_input_choise")}");
                            Title.Set($"{Lang.GetText("title_error")}");
                            Title.Wait();
                            break;
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                // Вывод сообщения об ошибке
                Console.Clear();
                Title.Set("Ошибка");
                Console.WriteLine(
                    "Возникла ошибка. Программа вернёт вас на Главную\n" +
                    "Чтобы не столкнуться с данной проблемой вновь, пишите в правильном формате!\n" +
                    $"[DEBUG]: {ex.Message}\n\n" +
                    "Если ничего не помогает, напишите нам об ошибке\n" +
                    "[link]https://discord.gg/JkkEKuT7P3[/link] (CTRL +  ПКМ по ссылке)"
                    );
                Title.Wait();
            }
        }
    }
}