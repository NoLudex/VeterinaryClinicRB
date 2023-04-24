using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace VeterinaryClinicRB
{
    class __main__
    {
        public static void Main(string[] args)
        {
            // Путь до баз данных
            string FilePathOrganization = "./database/organization.xml";
            string FilePathClientele = "./database/clientele.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(FilePathOrganization);

            int searchId = 1; // Индификатор искомого человека
            XmlNode ?personNode = doc.SelectSingleNode($"/Organization/Person[@id='{searchId}']");

            if (personNode != null)
            {
                string name = personNode.SelectSingleNode("Name")?.InnerText;
                int age = int.Parse(personNode.SelectSingleNode("Age")?.InnerText);
                string gender = personNode.SelectSingleNode("Gender")?.InnerText;
                System.Console.WriteLine($"{name}, {gender} {age} лет");
            }

            
        }
    }
}