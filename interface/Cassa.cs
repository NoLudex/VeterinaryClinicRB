using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Cassa
    {
        public static string MenuStr =
            "Меню связанное с кассой\n" +
            "Чтобы продолжить, нужно выбрать пункт ниже\n" +
            "1. Просмотр статистики кассы\n" +
            "2. Просмотр лотов кассы\n" +
            "3. Добавить лот приёма\n" +
            "4. Изменить лот приёма\n" +
            "5. Изменить статус лота\n" +
            "6. Удалить лот приёма\n" +
            "0. Выйти назад";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Касса");
                Console.Clear();
                Console.WriteLine(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        Cassa.GetStatistics();
                        break;
                    case 2:
                        XmlRead.Book("cassa", "cassa", "payment");
                        break;
                    case 3:
                        XmlAdd.New("cassa", "cassa", "payment");
                        break;
                    case 4:
                        int IDToUpdate = Convert.ToInt32(Valid.Number("Введите ID лота, чтобы изменить его (0 - отмена)\nВвод: "));
                        if (IDToUpdate == 0)
                            break;
                        XmlChange.Update("cassa", "cassa", "payment", IDToUpdate);
                        break;
                    case 5:
                        string IDToChangeStatus = Valid.Number("Введите ID лота, чтобы изменить его статус (0 - отмена)\nВвод: ");
                        if (IDToChangeStatus == "0")
                            break;
                        XmlChange.UpdatePaid(IDToChangeStatus);
                        break;
                    case 6:
                        int IDToDelete = Convert.ToInt32(Valid.Number("Введите ID лота, чтобы удалить его (0 - отмена)\nВвод: "));
                        if (IDToDelete == 0)
                            break;
                        if (Valid.Accept($"ID: {IDToDelete}"))
                            XmlDelete.This("cassa", "cassa", "payment", IDToDelete);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление лота было отменено.");
                            Title.Wait();
                        }
                        break;
                    case 0:
                        enableMenu = false;
                        break;
                    default:
                        enableMenu = false;
                        Console.Clear();
                        Console.WriteLine("Введите номер пунктта правильно!");
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}