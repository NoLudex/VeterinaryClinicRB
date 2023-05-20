using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
using System.Globalization;

namespace VeterinaryClinicRB
{
    public partial class Cassa
    {
        public static void GetStatistics()
        {
            XDocument doc = XDocument.Load("./database/cassa.xml");

            DateTime today = DateTime.Today;
            DateTime weekAge = today.AddDays(-7);
            
            Title.Set("Выручка клиники");

            double todayAmount = 0;
            double totalAmount = 0;
            double weekAmount = 0;
            int totalLots = 0;
            int paidLots = 0;

            foreach (XElement payment in doc.Descendants("payment"))
            {
                DateTime playmentDate = DateTime.ParseExact(payment.Element("date").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                double amount = (double)payment.Element("amount");
                int lotId = (int)payment.Element("id");
                string status = payment.Attribute("status").Value;

                if (playmentDate == today && status == "Оплачено") // Добавляет за сутки
                {
                    todayAmount += amount;
                    totalLots++;
                    paidLots++;
                }

                if (playmentDate >= weekAge && playmentDate <= today && status == "Оплачено") // Добавляет за неделю
                {
                    weekAmount += amount;
                    totalLots++;
                    paidLots++;
                }

                if (status == "Оплачено")
                {
                    totalAmount += amount;
                    paidLots++;
                }

                totalLots++;
            }
            
            int unpaidLots = totalLots - paidLots;

            Console.Clear();
            Console.WriteLine(
                $"Статистика выручки клиники ({today:dd.MM.yyyy})\n" + 
                $"Сегодня: {todayAmount:C2}\n" +
                $"Неделя: {weekAmount:C2}\n" +
                $"Выручка за все время: {totalAmount:C2}"
            );
            Title.Wait();
        }
    }
}