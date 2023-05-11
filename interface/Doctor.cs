using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Doctor
    {
        public static string MenuStr = 
            "Меню связанное с Врачами\n" +
            "Выберите действие из списка, которое желаете произвести\n" +
            "1. Открыть список Врачей\n" +
            "2. Показать Врача, указав ID\n" +
            "3. Изменить информацию Врача по ID\n" +
            "4. Добавить нового Врача в список\n" +
            "5. Удалить Врача, указав ID\n" +
            "6. Вывести статистику Врача по ФИО\n" +
            "0. Выйти в основное меню";
        public static void Menu()
        {   
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Меню врачи");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

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
                            Console.WriteLine("Удаление врача было отменено.");
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