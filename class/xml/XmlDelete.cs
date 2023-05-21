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
            Title.Set($"{Lang.GetText("title_delete_element", FileName)}.xml");
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
                    Console.WriteLine(Lang.GetText("string_wrong_other"));
                
                document.Save($"./database/{FileName}.xml");
                Console.WriteLine($"{Lang.GetText("delete_this_done", id, FileName)}");
                Title.Set(Lang.GetText("title_success"));
                Title.Wait();
            }
            else
            {
                Console.WriteLine($"{Lang.GetText("delete_this_error", id, FileName)}");
                Title.Set(Lang.GetText("title_error_simpl"));
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
            Console.WriteLine($"{Lang.GetText("delete_statistics_by_date", nodesToDelete.Count)}");
            Title.Wait();
            // Сохраняем изменения в xml-файле
            doc.Save(path);
        }
    }
}