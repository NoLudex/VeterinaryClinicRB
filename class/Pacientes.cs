using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Pacientes
    {
        public Pacientes() 
        { 
            Id = 0;
            AnimalType = "Неизвестно";
            Gender = 'n';
            Age = 0;
            Name = "Нет клички";
            FullnameOwner = "Нет владельца";
        }

        // Pacientes hui = new Pacientes(ID, AnimalType, Gender, Age, Name, FullnameOwner);

        // Конструктор класса
        public Pacientes(int ID, string AnimalType, char Gender, double Age, string Name, string FullnameOwner)
        {
            this.Id = ID;
            this.AnimalType = AnimalType;
            this.Gender = Gender;
            this.Age = Age;
            this.Name = Name;
            this.FullnameOwner = FullnameOwner;
        }

        // Класс ориентирован на запись объекта под БД
        public int Id { get; set; }
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
        public char Gender { get; set; }
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
