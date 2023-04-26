using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Pacientes
    {
        // Класс ориентирован на запись объекта под БД
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
        public string AnimalType
        {
            get
            {
                return AnimalType;
            }
            set
            {
                if (value == null)
                    AnimalType = "Неизвестное животное";
                else
                    AnimalType = value;
            }
        }
        public double Age
        {
            get
            {
                return Age;
            }
            set
            {
                if (value < 0)
                    Age = 0;
                else
                    Age = value;
            }
        }
        public string Name 
        { 
            get
            {
                return Name;
            } 
            set
            {
                if (value == null)
                    Name = "Неизвестное имя";
                else
                    Name = Convert.ToString(value);
            } 
        }
        public string FullnameOwner
        {
            get
            {
                return FullnameOwner;
            }
            set
            {
                if (value == null)
                    FullnameOwner = "Неизвестный владелец животного";
                else
                    Name = Convert.ToString(value);
            }
        }
    }
}
