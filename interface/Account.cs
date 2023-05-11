using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class Account
    {
        public static string MenuStr =
            "Меню связанное с вашим аккаунтом (ADMIN)\n" +
            "(Когда закончите с идеями для этой вкладки => буду делать)\n" +
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
                    default:
                        enableMenu = false;
                        break;
                }
            }
            return;
        }
    }
}