using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Pacientes
    {
        public static string MenuStr =
            "Меню связанное с Пациентами\n" +
            "Выберите действие из списка, которое желаете произвести\n" +
            "1. Открыть список Пациентов\n" +
            "2. Показать Пациента, указав ID\n" +
            "3. Изменить информацию Пациента по ID\n" +
            "4. Добавить нового Пациента в список\n" +
            "5. Удалить Пациента, указав ID\n" +
            "6. Изменить валидность Пациента, указать ID\n" +
            "7. Найти Пациента(ов) по кличке\n" +
            "0. Выйти в основное меню";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Меню пациентов");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

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
                            Console.WriteLine("Удаление пациента было отменено.");
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
                        Console.WriteLine("Ошибка! Правильно введите номер действия");
                        Title.Set("Ошибка");
                        Title.Wait();
                    break;
                }
            }
            return;
        }
    }
}