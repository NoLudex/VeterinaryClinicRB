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
            "Меню поиска по другим параметрам (Статистика)\n" +
            "Выберите фильтр для поиска статистики\n" +
            "1. Просмотр всех приёмов\n" +
            "2. По диапазону дат\n" +
            "3. По определённой дате\n" +
            "0. Вернуться в меню статистики";
        public static void SearchMenu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Меню поиска стат.");
                Console.Clear();
                Console.Write(SearchMenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        XmlRead.Book("admission", "admission", "animal");
                        break;
                    case 2:
                        Console.Clear();
                        string startTimeSrt = Valid.Date("(начальную)");
                        string endTimeSrt = Valid.Date("(конечную)");
                        DateTime startTime = DateTime.ParseExact(startTimeSrt, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        DateTime endTime = DateTime.ParseExact(endTimeSrt, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        XmlStat.FindAdmissions(startTime, endTime);
                        break;
                    case 3:
                        Console.Clear();
                        Title.Set("Просмотр приёма по дате");
                        string date = Valid.Date("");
                        Title.Set($"Просмотр приёмов {date}");
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