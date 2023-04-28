using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Xml;
using System.Xml.Linq;

namespace VeterinaryClinicRB
{
    public class XmlSort
    {
        public static void SortByDate(string xmlFilePath) 
        {
            XDocument xml = XDocument.Load(xmlFilePath);

            // Получаем список элементов "data" с датой и описание
            List<XElement> dataList = xml.Descendants("data").ToList();

            // Сортируем список элементов по убыванию даты
            dataList.Sort((a, b) => DateTime.Parse(b.Element("date").Value).CompareTo(DateTime.Parse(a.Element("date").Value)));

            // Удаляем все элементы "data" из XML-файла
            xml.Descendants("data").Remove();

            // Добавляем отсортированные элементы "data" в XML-файл
            xml.Root.Add(dataList);

            // Сохраняем изменение в XML-файле
            xml.Save(xmlFilePath);
        }
    }
}