using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Account
    {
        public static string MenuStr =
            "Меню связанное с данной сессией приложения\n" +
            "Выберите действие из списка, которое желаете произвести\n" +
            "1. Текущая учетная запись (NEW)\n" +
            "2. Изменить логин уч. записи (NEW)\n" +
            "3. Изменить пароль уч. записи (NEW)\n" +
            "4. Вкл / Выкл автоматический вход (NEW)\n" +
            "5. Сменить уч. запись (NEW)\n" +
            "6. Меню настроек ключа-доступа (NEW)\n" +
            "0. Вернуться в главное меню";
        public static void Menu()
        {
            bool enableMenu = true; 
            while (enableMenu)
            {
                Title.Set("Меню аккаунта");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        Config.Set("Login", "");
                        Config.Set("AutoLogin", false);
                        Console.WriteLine("Чтобы продолжить, перезапустите программу.");
                        Title.Wait();
                        Environment.Exit(0);
                        break;
                    case 6:
                        AccessKey.Menu(); // Включить меню настроек ключ-доступа
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