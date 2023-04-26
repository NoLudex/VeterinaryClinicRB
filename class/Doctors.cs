using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
      public class Doctors
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
                   FullnameDoctor = "Неизвестный врач";
                else
                   FullnameDoctor = Convert.ToString(value);

                
            }
        }
        public double birthday 
        {
            get
            {
                return birthday;
            }
            set 
            {
                if (value < 0)
                    birthday = 0;
                else 
                    birthday = value;
            }
        }
        public double Experience
        {
            get 
            {
                return Experience;
            }
            set 
            {
                if (value < 0)
                    Experience = 0;
                else 
                    Experience = value;
            }
        }
        public double CuredAnimals 
        {
            get
            {
                return CuredAnimals;
            }
            set 
            {
                if (value < 0)
                    CuredAnimals = 0;
                else
                    CuredAnimals = value;
            }
        }
     }
}

