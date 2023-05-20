using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class AccessKey
    {
        // Ошибка ключа
        public static void Error(int j)
        {
            Title.Set($"{Lang.GetText("title_access_error")}");

            int i = j;
            string dots = "";
            int counter = 0;
            while (i != 0)
            {
                ErrorWrite(dots, i);
                dots += ".";
                if (++counter == 4)
                {
                    dots = "";
                    counter = 0;
                }
                Thread.Sleep(550);
                ErrorWrite(dots, i);
                dots += ".";
                if (++counter == 4)
                {
                    dots = "";
                    counter = 0;
                }
                Thread.Sleep(550);
                i--;
            }
        }

        // Вынесение команд в отдельный метод, так легче
        public static void ErrorWrite(string dots, int i)
        {
            Console.Clear();
            Console.WriteLine($"{Lang.GetText("error_access_key")}");
            Console.Write($"{Lang.GetText("error_try_again", i)}" + dots + "\n");
        }
    }
}