using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;

namespace VeterinaryClinicRB
{
    partial class __main__
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            // Проверка валидности ключа
            if (!ValidateAccessKey())
            {
                Console.Clear();
                Console.Write("|enterKey|\n|input| ");
                string ?key = Console.ReadLine();
                if (!ValidateAccessKey(key))
                {
                    Console.Clear();
                    Console.WriteLine("|errorKey|");
                    Console.WriteLine("|anyKey|");
                    Console.ReadKey();
                    return;
                }
            }

            // Запуск цикла меню
            bool isRunning = true;

            try
            {
                while (isRunning)
                {
                    Console.Clear();
                    Console.Write(
                        "Программа [|programmName|]\n" +
                        "|choose|\n" +
                        "1. |showDoctors|\n" +
                        "2. |showDocID|\n" +
                        "3. |updInfDoc|\n" +
                        "4. |newDoctor|\n" +
                        "5. |delDoctor|\n" +
                        "6. |exit|\n" +
                        "|input| "
                    );
    
                    int choice = int.Parse(Console.ReadLine());
    
                    switch (choice)
                    {
                        case 1:
                            ReadXML("doctors", "doctors", "doctor");
                            break;
                        case 2:
                            Console.Write("|enterID| ");
                            int idToFind = int.Parse(Console.ReadLine());
                            ShowXMLByID("doctors", "doctors", "doctor", idToFind);
                            break;
                        case 3:
                            Console.Write("|enterID| ");
                            int idToUpdate = int.Parse(Console.ReadLine());
                            UpdateXML("doctors", "doctors", "doctor", idToUpdate);
                            break;
                        case 4:
                            AddXML("doctors", "doctors", "doctor");
                            break;
                        case 5:
                            Console.Write("|enterID| ");
                            int idToDelete = int.Parse(Console.ReadLine());
                            DeleteXML("doctors", "doctors", "doctor", idToDelete);
                            break;
                        case 6:
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("|errorChoise|");
                            break;
                    }
                    wait();
                }
            }
            catch (System.FormatException)
            {
                Console.Clear();
                Console.WriteLine("|errorInput|");
                wait();
            }
        }
    }
}