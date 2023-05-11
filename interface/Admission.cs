using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Admission
    {
        public static string MenuStr =
            "Меню связанное с Приёмами\n" +
            "Выберите действие из списка, которое желаете произвести\n" +
            "1. Открыть список Приёмов\n" +
            "2. Показать Приём, указав ID\n" +
            "3. Изменить информацию Приёма по ID\n" +
            "4. Добавить новый Приём в список\n" +
            "5. Удалить Приём, указав ID\n" +
            "6. Изменить информацию Приёма, указав ID\n" +
            "0. Выйти в основное меню";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Меню приёмов");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        XmlRead.Book("admission", "admission", "animal");
                        break;
                    case 2:
                        int IDToFind = Convert.ToInt32(Valid.Number("Введите ID приёма, чтобы просмотреть подробнее (0 - отмена)\nВвод: "));
                        if (IDToFind == 0)
                            break;
                        XmlRead.ShowById("admission", "admission", "animal", IDToFind);
                        break;
                    case 3:
                        int IDToUpdate = Convert.ToInt32(Valid.Number("Введите ID приёма, чтобы изменить его (0 - отмена)\nВвод: "));
                        if (IDToUpdate == 0)
                            break;
                        XmlChange.Update("admission", "admission", "animal", IDToUpdate);
                        break;
                    case 4:
                        XmlAdd.New("admission", "admission", "animal");
                        break;
                    case 5:
                        int IDToDelete = Convert.ToInt32(Valid.Number("Введите ID приёма, чтобы удалить его (0 - отмена)\nВвод: "));
                        if (IDToDelete == 0)
                            break;
                        if (Valid.Accept($"ID: {IDToDelete}"))
                            XmlDelete.This("admission", "admission", "animal", IDToDelete);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление приёма было отменено.");
                            Title.Wait();
                        }
                        break;
                    case 6:
                        string IDToChangeInfo = Valid.Number("Введите ID приёма, чтобы обновить его информацию (0 - отмена)\nВвод: ");
                        if (IDToChangeInfo == "0")
                            break;
                        XmlChange.UpdateInfo(IDToChangeInfo);
                        break;
                    case 0:
                        enableMenu = false;
                        break;
                    default:
                        enableMenu = false;
                        Console.Clear();
                        Console.WriteLine("Ошибка. Правильно введите номер действия!");
                        Title.Set("Ошибка");
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}