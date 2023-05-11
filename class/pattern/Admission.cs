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
    public partial class Admission
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int PacienteId { get; set; }
        public string FullnameDoctor { get; set; }
        public string Complaints { get; set; }
        public string Diagnosis { get; set; }
        public string Info { get; set; }

        public static List<Admission> Get()
        {
            XDocument document = XDocument.Load("./database/admission.xml");
            return document.Descendants("animal").Select(a => new Admission
            {
                Id = (int)a.Element("id"),
                DateTime = DateTime.ParseExact(a.Element("time").Value, "H:mm", null),
                PacienteId = (int)a.Element("paciente-id"),
                FullnameDoctor = a.Element("fullname-doctor").Value,
                Complaints = a.Element("complaints").Value,
                Diagnosis = a.Element("diagnosis").Value,
                Info = a.Element("info").Value
            }).ToList();
        }
    }
       
}

