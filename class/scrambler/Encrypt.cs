using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Encrypt
    {
        public static string Get(string text, int shift)
        {
            char[] chars = text.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (c >= 'a' && c <= 'z')
                    c = (char)((c - 'a' + shift) % 26 + 'a');
                else if (c >= 'A' && c <= 'Z')
                    c = (char) ((c - 'A' + shift) % 26 + 'A');
                chars[i] = c;
            }
            string result = new string(chars);
            if (shift > 3)
                return result.Substring(0, result.Length - 2);
            else
                return result + "==";
        }
    }
}