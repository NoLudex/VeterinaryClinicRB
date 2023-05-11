using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace VeterinaryClinicRB
{
    public partial class Statistic { 
        private XDocument xmlDocument;

        public Statistic(string xmlFilePath)
        {
            xmlDocument = XDocument.Load(xmlFilePath);
        }

        public int GetTodayCount()
        {
            string today = DateTime.Today.ToString(xmlDocument.Element("statistic").Attribute("dateformat").Value);
            
            var todayData = xmlDocument.Descendants("data")
                .Where(d => d.Element("date").Value == today);
            
            return todayData.Count();
        }
    }
}
