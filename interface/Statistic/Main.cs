using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Statistic
    {
        public static string MenuStr =
            $"{Lang.GetText("statistic_menu_line")}\n" +
            $"{Lang.GetText("general_menu_0")}\n" +
            $"1. {Lang.GetText("statistic_choice_1")}\n" +
            $"2. {Lang.GetText("statistic_choice_2")}\n" +
            $"3. {Lang.GetText("statistic_choice_3")}\n" +
            $"4. {Lang.GetText("statistic_choice_4")}\n" +
            $"5. {Lang.GetText("statistic_choice_5")}\n" +
            $"6. {Lang.GetText("statistic_choice_6")}\n" +
            $"0. {Lang.GetText("string_back_to_main_menu")}";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_statistic")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1:
                        XmlRead.Book("statistic", "statistic", "data");
                        break;
                    case 2:
                        string data = Valid.Date("");
                        if (Valid.Accept($"{Lang.GetText("date")}: {data}"))
                            XmlDelete.StatisticsByDate(data);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("statistic_delete_close")}");
                            Title.Wait();
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("statistic_sorting")}...");
                        XmlSort.SortByDate("./database/statistic.xml");
                        Console.WriteLine($"{Lang.GetText("title_success")}");
                        Title.Set($"{Lang.GetText("title_success")}");
                        Title.Wait();
                        break;
                    case 4:
                        Console.Clear();
                        XmlSort.SortByDate("./database/statistic.xml");
                        Title.Set($"{Lang.GetText("title_all_events")}");
                        Console.WriteLine(XmlStat.GetEventsToday());
                        Title.Wait();
                        break;
                    case 5:
                        Statistic.SearchMenu(); // Другое меню (Search.cs)
                        break;
                    case 6:
                        Console.Clear();
                        Console.Write($"{Lang.GetText("statistic_input_type_animal")}: ");
                        string? animalType = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(animalType) || !string.IsNullOrWhiteSpace(animalType))
                            XmlStat.GetStatsByAnimalType(animalType);
                        else
                        {
                            Console.Clear();
                            Title.Set($"{Lang.GetText("title_error_put")}");
                            Console.WriteLine($"{Lang.GetText("string_error_input")}");
                            Title.Wait();
                        }
                        break;
                    case 0:
                        enableMenu = false;
                        break;
                    default:
                        enableMenu = false;
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("string_error_input_choise")}");
                        Title.Set($"{Lang.GetText("title_error_simpl")}");
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}