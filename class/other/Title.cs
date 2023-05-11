using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Title
    {
        // Выставление названия консоли с именем {NAME}
        public static void Set(string NAME)
        {
            Console.Title = "Ветеринарная клиника | " + NAME;
        }

        // Обычный обработчик, который ждёт подтверждения
        public static void Wait()
        {
            Console.Write("Нажмите Enter, чтобы продолжить...");
            Console.ReadKey();
        }
    }
}