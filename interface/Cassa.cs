using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Cassa
    {
        public static string MenuStr =
            $"{Lang.GetText("Cassa_menu_0")}\n" +
            $"{Lang.GetText("Cassa_menu_1")}\n" +
            $"1.{Lang.GetText("Cassa_choice_2")}\n" +
            $"2.{Lang.GetText("Cassa_choice_3")}\n" +
            $"3.{Lang.GetText("Cassa_choice_4")}\n" +
            $"4.{Lang.GetText("Cassa_choice_5")}\n" +
            $"5.{Lang.GetText("Cassa_choice_6")}\n" +
            $"0.{Lang.GetText("string_back_to_main_menu")}";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_cassa")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1337666228:
                        Cassa myCassa = new Cassa("./database/cassa.xml");
                        myCassa.GetStatistics();
                        break;
                    case 1:
                        XmlRead.Book("cassa", "cassa", "payment");
                        break;
                    case 2:
                        XmlAdd.New("cassa", "cassa", "payment");
                        break;
                    case 3:
                        int IDToUpdate = Convert.ToInt32(Valid.Number($"{Lang.GetText("cassa_menu_change")}\n{Lang.GetText("string_input")}: "));
                        if (IDToUpdate == 0)
                            break;
                        XmlChange.Update("cassa", "cassa", "payment", IDToUpdate);
                        break;
                    case 4:
                        string IDToChangeStatus = Valid.Number($"{Lang.GetText("cassa_menu_change_status")}\n{Lang.GetText("string_input")}: ");
                        if (IDToChangeStatus == "0")
                            break;
                        XmlChange.UpdatePaid(IDToChangeStatus);
                        break;
                    case 5:
                        int IDToDelete = Convert.ToInt32(Valid.Number($"{Lang.GetText("cassa_menu_delete")}\n{Lang.GetText("string_input")}: "));
                        if (IDToDelete == 0)
                            break;
                        if (Valid.Accept($"ID: {IDToDelete}"))
                            XmlDelete.This("cassa", "cassa", "payment", IDToDelete);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("Cassa_delete_cancel")}");
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
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}