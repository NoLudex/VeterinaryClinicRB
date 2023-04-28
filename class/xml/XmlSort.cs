using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace VeterinaryClinicRB
{
    public class XmlSort
    {
        public static void SortByDate(string xmlFilePath) 
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            // Выбираем все элементы "data", сортируем их по убыванию даты
            var sortedData = doc.Descendants("data")
                                .OrderByDescending(e => DateTime.ParseExact(e.Element("date").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                                .ToList();

            // Создаем новый элемент "statistic"
            XElement statistic = new XElement("statistic");

            // Добавляем отсортированные элементы "data" в "statistic"
            foreach (XElement data in sortedData)
            {
                statistic.Add(data);
            }

            // Заменяем старый "statistic" на новый
            doc.Element("statistic").ReplaceWith(statistic);

            doc.Save(xmlFilePath);
        }
    }
}