using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Pacientes
    {
        public static string MenuStr =
            $"{Lang.GetText("Patientes_menu_0")}\n" +
            $"{Lang.GetText("string_choise")}\n" +
            $"1. {Lang.GetText("Patientes_choice_1")}\n" +
            $"2. {Lang.GetText("Patientes_choice_2")}\n" +
            $"3. {Lang.GetText("Patientes_choice_3")}\n" +
            $"4. {Lang.GetText("Patientes_choice_4")}\n" +
            $"5. {Lang.GetText("Patientes_choice_5")}\n" +
            $"6. {Lang.GetText("Patientes_choice_6")}\n" +
            $"7. {Lang.GetText("Patientes_choice_7")}\n" +
            $"0. {Lang.GetText("string_back_to_main_menu")}";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set($"{Lang.GetText("title_patientes")}");
                Console.Clear();
                Console.Write(MenuStr + $"\n{Lang.GetText("string_input")}: ");

                switch (Choice.Get())
                {
                    case 1:
                        XmlRead.Book("pacientes", "pacientes", "paciente");
                        break;
                    case 2:
                        int IDToFind = Convert.ToInt32(Valid.Number("Введите ID пациента, чтобы просмотреть его профиль (0 - отмена)\nВвод: "));
                        if (IDToFind == 0)
                            break;
                        XmlRead.ShowById("pacientes", "pacientes", "paciente", IDToFind);
                        break;
                    case 3:
                        int IDToUpdate = Convert.ToInt32(Valid.Number("Введите ID пациента, чтобы обновить его данные (0 - отмена)\nВвод: "));
                        if (IDToUpdate == 0)
                            break;
                        XmlChange.Update("pacientes", "pacientes", "paciente", IDToUpdate);
                        break;
                    case 4:
                        XmlAdd.New("pacientes", "pacientes", "paciente");
                        break;
                    case 5:
                        int IDToDelete = Convert.ToInt32(Valid.Number("Введите ID пациента, чтобы удалить его (0 - отмена)\nВвод: "));
                        if (IDToDelete == 0)
                            break;
                        if (Valid.Accept($"ID: {IDToDelete}"))
                            XmlDelete.This("pacientes", "pacientes", "paciente", IDToDelete);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("Patientes_delete_cancel")}");
                            Title.Wait();
                        }
                        break;
                    case 6:
                        string IDToChangeValid = Valid.Number("Введите ID пациента, чтобы изменить его валидность (0 - отмена)\nВвод: ");
                        if (IDToChangeValid == "0")
                            break;
                        XmlChange.UpdateValid(IDToChangeValid);
                        break;
                    case 7:
                        XmlRead.FindPacientByName();
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