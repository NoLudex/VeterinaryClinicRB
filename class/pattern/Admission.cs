using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Admission
    {
        public string id {get; set;}
        public string dateTime {get; set;}
        public  string fullnameDoctor {get; set;}
        public string complaints {get; set;}
        public string diagnosis {get; set;}
        public string info {get; set;}

        // Конструктор класса
        public Admission(string id, string dateTime, string fullnameDoctor, string complaints, string diagnosis, string info )
        {
            // Присвоение значений
            this.id = id;
            this.dateTime = dateTime;
            this.fullnameDoctor = fullnameDoctor;
            this.complaints = complaints;
            this.diagnosis = diagnosis;
            this.info = info;
        }
        public Admission()
        {
            // Присвоение значений, если их нет
            this.id = "0";
            this.dateTime = "Дата и Время неизвестны";
            this.fullnameDoctor = "Неизвестно";
            this.complaints = "Неопределено";
            this.diagnosis = "Неопределен";
            this.info = "Информация отсутствует";
        }
    } 
       
}

