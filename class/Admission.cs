using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Admission
    {
        public int Id
        {
            get
            {
                return Id;
            }
            set
            {
                if (value < 0)
                    Id = 0;
                else
                    Id = value;
            } 
        }  

        public string FullnameDoctor
        {
            get
            {
                return FullnameDoctor;
            }
            set
            {
                if (value == null)
                    FullnameDoctor = "Неизвестно";
                else
                    FullnameDoctor = value;
            }
        }
        
        public string Datetime
        {
            get
            {
                return Datetime;
            }
            set
            {
                if (value == null)
                    Datetime = "Неизвестная дата";
                else
                    Datetime = value;
            }
        }

        public string Complaints
        {
            get
            {
                return Complaints;
            }
            set
            {
                if (value == null)
                    Complaints = "Неизвестная дата";
                else
                    Complaints = value;
            }
        }

        public string Diagnosis
        {
            get
            {
                return Diagnosis;
            }
            set
            {
                if (value == null)
                    Diagnosis = "Неизвестная дата";
                else
                    Diagnosis = value;
            }
        }

        public string Info
        {
            get
            {
                return Info;
            }
            set
            {
                if (value == null)
                    Info = "Неизвестная дата";
                else
                    Info = value;
            }
        }
    } 
}

