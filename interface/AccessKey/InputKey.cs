using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class AccessKey
    {
        public static bool InputKey(string key)
        {
            if (CheckAccess(key))
                return true;
            else
            {
                Console.Clear();
                Title.Set("Верификация");
                Console.Write(
                    "Чтобы иметь доступ к программе нужен специальный ключ.\n" +
                    "Вы можете обратиться к администрации для получения данного ключа\n" +
                    "Ввод: "
                    );
                return false;
            }
        }
    }
}