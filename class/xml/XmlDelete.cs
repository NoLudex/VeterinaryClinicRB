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
        // Отдельное удаление элементов для статистики
        public static void DeleteStatistic(string date)
        {
            // Загрузка XML-документа 
            XmlDocument doc = new XmlDocument();
            doc.Load("./database/statistic.xml");
            
            // Выбор всех элементов, содержащих заданную дату
            XmlNodeList nodesToDelete = doc.SelectNodes($"//data[='{date}']");

            // Удаление выбранных элементов
            int deletedCount = nodesToDelete.Count;
            foreach (XmlNode node in nodesToDelete)
                node.ParentNode.RemoveChild(node);
            
            Console.Clear();
            // Проверка наличия найденных элементов и вывод уведомления
            if (deletedCount == 0)
                Console.WriteLine($"Не найдено элементов, связанные с данной датой");
            else
                Console.WriteLine($"Удалено элементов: {deletedCount}");
            Title.Set("Удаление завершено");
            Title.Wait();
            // Сохраняем
            doc.Save("./database/statistic.xml");
        }
    }
}