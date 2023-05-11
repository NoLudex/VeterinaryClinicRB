using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Statistic
    {
        public static string MenuStr =
            "Меню связанное со статистикой\n" +
            "Выберите действие из списка, которое желаете произвести\n" +
            "1. Открыть список/статистику по дням\n" +
            "2. Удалить статистику дня\n" +
            "3. Сделать сортировку статистики\n" +
            "4. Статистика с количеством событий\n" +
            "5. Другие параметры просмотра статистики (NEW)\n" +
            "6. Статистика по виду животного (NEW)\n" +
            "0. Выйти в основное меню";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Меню статистики");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        XmlRead.Book("statistic", "statistic", "data");
                        break;
                    case 2:
                        string data = Valid.Date("");
                        if (Valid.Accept($"DATE: {data}"))
                            XmlDelete.StatisticsByDate(data);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление статистики дня было отменено.");
                            Title.Wait();
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Производится сортировка...");
                        XmlSort.SortByDate("./database/statistic.xml");
                        Console.WriteLine("Успешно!");
                        Title.Set("Успех!");
                        Title.Wait();
                        break;
                    case 4:
                        Console.Clear();
                        XmlSort.SortByDate("./database/statistic.xml");
                        Title.Set("Все события");
                        Console.WriteLine(XmlStat.GetEventsToday());
                        Title.Wait();
                        break;
                    case 5:
                        Statistic.SearchMenu(); // Другое меню (Search.cs)
                        break;
                    case 6:
                        Console.Clear();
                        Console.Write("Введите вид животного: ");
                        string? animalType = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(animalType) || !string.IsNullOrWhiteSpace(animalType))
                            XmlStat.GetStatsByAnimalType(animalType);
                        else
                        {
                            Console.Clear();
                            Title.Set("Ошибка ввода");
                            Console.WriteLine("Неверный формат, попробуйте снова");
                            Title.Wait();
                        }
                        break;
                    case 0:
                        enableMenu = false;
                        break;
                    default:
                        enableMenu = false;
                        Console.Clear();
                        Console.WriteLine("Правильно введите номер действия");
                        Title.Set("Ошибка");
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}