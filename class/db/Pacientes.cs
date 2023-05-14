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
    public partial class Paciente
    {
        public int Id { get; set; }
        public string AnimalType { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string FullnameOwner { get; set; }
        public string IsValid { get; set; }
        public string TelegramId { get; set; }
        public static List<Paciente> Get()
        {
            XDocument document = XDocument.Load("./database/pacientes.xml");
            return document.Descendants("paciente").Select(p => new Paciente
            {
                Id = (int)p.Element("id"),
                AnimalType = p.Element("animal-type").Value,
                Name = p.Element("name").Value,
                Gender = p.Element("gender").Value,
                Age = (int)p.Element("age"),
                FullnameOwner = p.Element("fullname-owner").Value,
                IsValid = p.Element("valid").Value,
                TelegramId = p.Element("telegram-id").Value
            }).ToList();
        }
    }
}

    