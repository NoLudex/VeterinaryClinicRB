using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace VeterinaryClinicRB
{
    class __main__
    {
        public static void Main(string[] args)
        {
            List<Pacientes> client = new List<Pacientes>();

            // Pacientes p1 = new Pacientes { 3, "Скунс", "Олег", 'F', 6, "Олежек Вячеславович"};
            Pacientes p1 = new Pacientes();
            p1.Id = 3;
            p1.AnimalType = "Капибара";
            p1.Name = "Рогалик";
            p1.Gender = 'M';
            p1.Age = 3;
            p1.FullnameOwner = "Олежек Вячеславович";
            client.Add(p1);

            string PathFilePacients = "./database/pacientes.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Pacientes>));
            using (TextWriter writer = new StreamWriter(PathFilePacients))
            {
                xmlSerializer.Serialize(writer, client);
            }
            
        }
    }
}