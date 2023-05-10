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
        public static void This(string FileName, string MainTag, string ObjectTag, int id)
        {   
            Title.Set($"Удаление элемента в {FileName}.xml");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            XmlNode ?FileNameNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}[id='{id}']");
            Console.Clear();
            if (FileNameNode != null)
            {
                XmlNode ?parentNode = FileNameNode.ParentNode;
                if (parentNode != null)
                    parentNode.RemoveChild(FileNameNode);
                else
                    Console.WriteLine("[DEBUG] Произошла внутренняя ошибка");
                
                document.Save($"./database/{FileName}.xml");
                Console.WriteLine($"Успешно удалена секция под ID: {id}, в файле {FileName}.xml");
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
        // Отдельное удаление элементов для статистики
        public static void StatisticsByDate(string date)
        {
            // Загрузка XML-документа 
            string path = "./database/statistic.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            // Выбираем все элементы, содержащие заданную дату
            XmlNodeList nodesToDelete = doc.SelectNodes($"//data[date='{date}']");

            // Удаляем все найденные элементы
            foreach (XmlNode node in nodesToDelete)
            {
                node.ParentNode.RemoveChild(node);
            }

            // Сообщаем пользователю, сколько элементов было удалено
            Console.Clear();
            Console.WriteLine($"Удалено элементов: {nodesToDelete.Count}");
            Title.Wait();
            // Сохраняем изменения в xml-файле
            doc.Save(path);
        }
    }
}