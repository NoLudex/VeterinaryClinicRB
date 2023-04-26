using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Pacientes
    {
        public string id {get; set;}
        public string name {get; set;}
        public string gender {get; set;}
        public string age {get; set;}
        public string fullnameOwner {get; set;}
        public string valid {get; set;}
        public string telegramId { get; set;}

        public Pacientes(string id, string name, string gender, string age, string fullnameOwner, string valid, string telegramId)
        {
            this.id = id;
            this.name = name;
            this.gender=gender;
            this.age=age;
            this.fullnameOwner=fullnameOwner;
            this.valid=valid;
            this.telegramId=telegramId;
        }
        public Pacientes()
        {
            this.id = "0";
            this.name = "Неизвестная Кличка";
            this.gender = "Неопределенный пол";
            this.age = "Неопределенный возраст" ;
            this.fullnameOwner = "Неизвестно";
            this.valid = "Неопределено" ;
            this.telegramId = "TG: ID не задан";
        }
    }
}

    