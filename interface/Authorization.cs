using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Authorization
    {
        public static string MenuStr = 
            "Для того, чтобы войти в приложение, нужно авторизироваться\n" +
            "Вы можете зарегистрировать аккаунт, если вы есть в базе данных врачей\n" +
            "Выберите действие из списка, которое желаете сделать\n" +
            "1. Авторизация существующей уч. записи\n" +
            "2. Регистрация врача, который не проходил регистрацию\n" +
            "3. Меню настроек ключ-доступа\n" +
            "0. Выход из приложения";
        public static void Menu()
        {
            bool enableMenu = true;
            while (enableMenu)
            {
                Title.Set("Авторизация");
                Console.Clear();
                Console.Write(MenuStr + "\nВвод: ");

                switch (Choice.Get())
                {
                    case 1:
                        string? fullname = Authorization.ValidateUser(Config.Get("AutoLogin", true));
                        if (fullname != "")
                            General.Menu(fullname);
                        break;
                    case 2:
                        Authorization.Register();
                        break;
                    case 3:
                        AccessKey.Menu();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Приложение будет закрыто");
                        Title.Wait();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ошибка ввода действия! Вы будете возвращены в меню авторизации");
                        Title.Set("Ошибка действия");
                        Title.Wait();
                        break;
                }
            }
        }
    }
}