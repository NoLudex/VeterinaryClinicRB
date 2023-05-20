using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Doctor
    {
        public static string MenuStr = 
            $"{Lang.GetText("Doctors_menu_1")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("Doctors_choice_1")}\n" +
            $"2. {Lang.GetText("Doctors_choice_2")}\n" +
            $"3. {Lang.GetText("Doctors_choice_3")}\n" +
            $"4. {Lang.GetText("Doctors_choice_4")}\n" +
            $"5. {Lang.GetText("Doctors_choice_5")}\n" +
            $"6. {Lang.GetText("Doctors_choice_6")}\n" +
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
                        int IDToFind = Convert.ToInt32(Valid.Number("Введите ID врача, чтобы просмотреть его профиль (0 - отмена)\nВвод: "));
                        if (IDToFind == 0)
                            break;
                        XmlRead.ShowById("doctors", "doctors", "doctor", IDToFind);
                        break;
                    case 3:
                        int IDToUpdate = Convert.ToInt32(Valid.Number("Введите ID врача, чтобы изменить его (0 - отмена)\nВвод: "));
                        if (IDToUpdate == 0)
                            break;
                        XmlChange.Update("doctors", "doctors", "doctor", IDToUpdate);
                        break;
                    case 4:
                        XmlAdd.New("doctors", "doctors", "doctor");
                        break;
                    case 5:
                        int IDToDelete = Convert.ToInt32(Valid.Number("Введите ID врача, чтобы удалить его (0 - отмена)\nВвод: "));
                        if (IDToDelete == 0)
                            break;
                        if (Valid.Accept($"ID: {IDToDelete}"))
                            XmlDelete.This("doctors", "doctors", "doctor", IDToDelete);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("Doctors_delete_cancel")}");
                            Title.Wait();
                        }
                        break;
                    case 6:
                        string FindDoctor = Valid.FullNameUser("врача");
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