using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Title
    {
        public static void Set(string NAME)
        {
            Console.Title = "Ветеринарная клиника | " + NAME;
        }
        public static void Wait()
        {
            Console.Write("Нажмите Enter, чтобы продолжить...");
            Console.ReadKey();
        }
    }
}