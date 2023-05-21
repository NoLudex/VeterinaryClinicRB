using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Doctor
    {
        public static string MenuStr = 
            $"{Lang.GetText("doctors_menu_1")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("doctors_choice_1")}\n" +
            $"2. {Lang.GetText("doctors_choice_2")}\n" +
            $"3. {Lang.GetText("doctors_choice_3")}\n" +
            $"4. {Lang.GetText("doctors_choice_4")}\n" +
            $"5. {Lang.GetText("doctors_choice_5")}\n" +
            $"6. {Lang.GetText("doctors_choice_6")}\n" +
            $"0. {Lang.GetText("string_back_to_main_menu")}";
        public static void Menu()
        {   
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_doctors")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1:
                        XmlRead.Book("doctors", "doctors", "doctor");
                        break;
                    case 2:
                        int IDToFind = Convert.ToInt32(Valid.Number($"{Lang.GetText("doctors_menu_info")}\n{Lang.GetText("string_input")}: "));
                        if (IDToFind == 0)
                            break;
                        XmlRead.ShowById("doctors", "doctors", "doctor", IDToFind);
                        break;
                    case 3:
                        int IDToUpdate = Convert.ToInt32(Valid.Number($"{Lang.GetText("doctors_menu_update")}\n{Lang.GetText("string_input")}: "));
                        if (IDToUpdate == 0)
                            break;
                        XmlChange.Update("doctors", "doctors", "doctor", IDToUpdate);
                        break;
                    case 4:
                        XmlAdd.New("doctors", "doctors", "doctor");
                        break;
                    case 5:
                        int IDToDelete = Convert.ToInt32(Valid.Number($"{Lang.GetText("doctors_menu_delete")}\n{Lang.GetText("string_input")}: "));
                        if (IDToDelete == 0)
                            break;
                        if (Valid.Accept($"ID: {IDToDelete}"))
                            XmlDelete.This("doctors", "doctors", "doctor", IDToDelete);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("doctors_delete_cancel")}");
                            Title.Wait();
                        }
                        break;
                    case 6:
                        string FindDoctor = Valid.FullNameUser(Lang.GetText("valid_full_name"));
                        XmlStat.FindDoctorStats(FindDoctor);
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