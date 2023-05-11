using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Decrypt
    {
        public static string Get(string text, int shift)
        {
            return Encrypt.Get(text, 26 - shift);
        }
    }
}