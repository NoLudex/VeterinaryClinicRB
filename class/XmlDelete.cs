using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;

namespace VeterinaryClinicRB
{
    public class XmlDelete
    {
        // Удаление Элемента
        static void DeleteXML (string FileName, string MainTag, string ObjectTag, int id)
        {   
            Title.Set($"Удаление элемента в {FileName}.xml");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            XmlNode ?FileNameNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}[id='{id}']");

            if (FileNameNode != null)
            {
                XmlNode ?parentNode = FileNameNode.ParentNode;
                if (parentNode != null)
                    parentNode.RemoveChild(FileNameNode);
                else
                    Console.WriteLine("[DEBUG] Произошла внутренняя ошибка");
                
                document.Save($"./database/{FileName}");
                Console.WriteLine($"Успешно удалена секцая под ID: {id}, в файле {FileName}.xml");
                Title.Set("Успех!");
                Title.Wait();
            }
            else
            {
                Console.WriteLine($"Не найдена секция под ID ({id}) в файле {FileName}.xml");
                Title.Set("Ошибка");
                Title.Wait();
            }
        }

    }
}