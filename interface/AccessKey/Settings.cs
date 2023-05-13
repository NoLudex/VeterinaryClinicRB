using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class AccessKey
    {
        public static string MenuStr =
            "Меню настроек ключа доступа\n" +
            "Выберите действие из списка, которое желаете произвести\n" +
            "1. Вкл / Выкл автоматический ввод ключа при входе (NEW)\n" +
            "2. Удалить сохранённый ключ (NEW)\n" +
            "3. Посмотреть текущий ключ доступа (NEW)\n" +
            "0. Вернуться в меню аккаунта";
        
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Меню ключа доступа");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        AccessKey.AutoCheckKey();
                        break;
                    case 2:
                        AccessKey.Delete();
                        break;
                    case 3:
                        AccessKey.Get();
                        break;
                    case 0:
                        enableMenu = false;
                        break;
                    default:
                        enableMenu = false;
                        Console.Clear();
                        Console.WriteLine("Ошибка ввода действия, возвращаем в предыдущее меню!");
                        Title.Set("Ошибка действия");
                        Title.Wait();
                        break;
                }
            }
            return;
        }
    }
}