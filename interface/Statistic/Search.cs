using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Statistic
    {
        public static string SearchMenuStr =
            $"{Lang.GetText("stat_menu_line_0")}\n" +
            $"{Lang.GetText("stat_menu_line_1")}\n" +
            $"1. {Lang.GetText("stat_choice_1")}\n" +
            $"2. {Lang.GetText("stat_choice_2")}\n" +
            $"3. {Lang.GetText("stat_choice_3")}\n" +
            $"0. {Lang.GetText("stat_choice_0")}";
        public static void SearchMenu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_stat")}.");
                Console.Clear();
                Console.Write(SearchMenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1:
                        XmlRead.Book("admission", "admission", "animal");
                        break;
                    case 2:
                        Console.Clear();
                        string startTimeSrt = Valid.Date($"({Lang.GetText("start_stat")})");
                        string endTimeSrt = Valid.Date($"({Lang.GetText("start_end")})");
                        DateTime startTime = DateTime.ParseExact(startTimeSrt, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        DateTime endTime = DateTime.ParseExact(endTimeSrt, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        XmlStat.FindAdmissions(startTime, endTime);
                        break;
                    case 3:
                        Console.Clear();
                        Title.Set($"{Lang.GetText("stat_check")}");
                        string date = Valid.Date("");
                        Title.Set($"{Lang.GetText("stat_check_date", date)}");
                        XmlStat.SearchByDate(date);
                        break;
                    default:
                        enableMenu = false;
                        break;
                }
                Title.Wait();
            }
            return;
        }
    }
}