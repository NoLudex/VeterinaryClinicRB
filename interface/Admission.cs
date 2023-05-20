using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Admission
    {
        public static string MenuStr =
            $"{Lang.GetText("Admissions_menu_1")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("Admissions_choise_1")}\n" +
            $"2. {Lang.GetText("Admissions_choise_2")}\n" +
            $"3. {Lang.GetText("Admissions_choise_3")}\n" +
            $"4. {Lang.GetText("Admissions_choise_4")}\n" +
            $"5. {Lang.GetText("Admissions_choise_5")}\n" +
            $"6. {Lang.GetText("Admissions_choise_6")}\n" +
            $"0. {Lang.GetText("string_back_to_main_menu")}";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_admissions")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

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
                            Console.WriteLine($"{Lang.GetText("Addmissions_delete_cancel")}");
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