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
        // public static void GetStatistics()
        // {
        //     XDocument doc = XDocument.Load("./database/cassa.xml");

        //     DateTime today = DateTime.Today;
        //     DateTime weekAge = today.AddDays(-7);
            
        //     Title.Set("Выручка клиники");

        //     double todayAmount = 0;
        //     double totalAmount = 0;
        //     double weekAmount = 0;
        //     int totalLots = 0;
        //     int paidLots = 0;

        //     foreach (XElement payment in doc.Descendants("payment"))
        //     {
        //         DateTime playmentDate = DateTime.ParseExact(payment.Element("date").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        //         double amount = (double)payment.Element("amount");
        //         int lotId = (int)payment.Element("id");
        //         string status = payment.Attribute("status").Value;

        //         if (playmentDate == today && status == "Оплачено") // Добавляет за сутки
        //         {
        //             todayAmount += amount;
        //             totalLots++;
        //             paidLots++;
        //         }

        //         if (playmentDate >= weekAge && playmentDate <= today && status == "Оплачено") // Добавляет за неделю
        //         {
        //             weekAmount += amount;
        //             totalLots++;
        //             paidLots++;
        //         }

        //         if (status == "Оплачено")
        //         {
        //             totalAmount += amount;
        //             paidLots++;
        //         }

        //         totalLots++;
        //     }
            
        //     int unpaidLots = totalLots - paidLots;

        //     Console.Clear();
        //     Console.WriteLine(
        //         $"Статистика выручки клиники ({today:dd.MM.yyyy})\n" + 
        //         $"Сегодня: {todayAmount:C2}\n" +
        //         $"Неделя: {weekAmount:C2}\n" +
        //         $"Выручка за все время: {totalAmount:C2}"
        //     );
        //     Title.Wait();
        // }

        private readonly string xmlFile;
        private readonly DateTime todayDate;
        private readonly DateTime weekAgoDate;
        private readonly DateTime monthAgoDate;

        public Cassa(string xmlFile)
        {
            this.xmlFile = xmlFile;
            this.todayDate = DateTime.Now.Date;
            this.weekAgoDate = this.todayDate.AddDays(-7);
            this.monthAgoDate = this.todayDate.AddDays(-30);
        }

        private List<double> GetPaymentsByStatus(string status)
        {
            XDocument xmlDoc = XDocument.Load(this.xmlFile);
            List<double> payments = new List<double>();

            foreach (XElement payment in xmlDoc.Descendants("payment").Where(x => x.Element("status").Value == status))
                payments.Add(Double.Parse(payment.Element("amount").Value));
            
            return payments;
        }

        private List<double> GetPaymentsByDateRange(DateTime startDate, DateTime endDate)
        {
            XDocument xmlDoc = XDocument.Load(this.xmlFile);
            List<double> payments = new List<double>();

            foreach (XElement payment in xmlDoc.Descendants("payment"))
            {
                DateTime paymentDate = DateTime.ParseExact(payment.Element("date").Value, "dd.MM.yyyy", null);

                if (paymentDate >= startDate && paymentDate <= endDate)
                    payments.Add(Double.Parse(payment.Element("amount").Value));
            }
            
            return payments;
        }

        private double GetTotalPayment(List<double> payments)
        {
            return Math.Round(payments.Sum(), 2);
        }

        public void GetStatistics()
        {
            List<double> todayPayments = GetPaymentsByDateRange(this.todayDate, this.todayDate);
            double todayPaid = GetTotalPayment(GetPaymentsByStatus("Оплачен"));
            double todayUnpaid = GetTotalPayment (todayPayments) - todayPaid;

            List<double> weekPayments = GetPaymentsByDateRange(this.weekAgoDate, this.todayDate);
            double weekPaid = GetTotalPayment(GetPaymentsByStatus("Оплачен"));
            double weekUnpaid = GetTotalPayment (weekPayments) - todayPaid;

            List<double> monthPayments = GetPaymentsByDateRange(this.monthAgoDate, this.todayDate);
            double monthPaid = GetTotalPayment(GetPaymentsByStatus("Оплачен"));
            double monthUnpaid = GetTotalPayment (monthPayments) - todayPaid;

            double allTimePaid = GetTotalPayment(GetPaymentsByStatus("Оплачен"));
            double allTimeUnpaid = GetTotalPayment(GetPaymentsByDateRange(DateTime.MinValue, this.todayDate)) - allTimePaid;

            Console.Clear();
            Console.WriteLine(
                "Статистика кассы:\n" +
                $"| За сегодня (1 день): Оплачено - {todayPaid}, не оплачено - {todayUnpaid} | всего: {GetTotalPayment(todayPayments)}\n" +
                $"| За последнюю наделю (7 дней): Оплачено - {weekPaid}, не оплачено - {weekUnpaid} | всего: {GetTotalPayment(weekPayments)}\n" +
                $"| За последний месяц (30 дней): Оплачено - {monthPaid}, не оплачено - {monthUnpaid} | всего: {GetTotalPayment(monthPayments)}\n" +
                $"| За всё время: Оплачено - {allTimePaid}, не оплачено - {allTimeUnpaid} | всего: {GetTotalPayment(GetPaymentsByDateRange(DateTime.MinValue, this.todayDate))}"
            ); // это одна переменная!!! => {GetTotalPayment(GetPaymentsByDateRange(DateTime.MinValue, this.todayDate))}
            Title.Wait();
        }
    }
}