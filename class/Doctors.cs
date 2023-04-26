using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public class Doctors
    {
        // Параметры, которые принимаются (Нужно, чтобы был красивый вывод информации)
        // ПОМЕТКА!!! ЭТИ (11-15) КОММЕНТАРИИ МОЖНО НЕ ДОБАВЛЯТЬ В ДРУГИЕ ФАЙЛЫ, при завершении работы с классами, в данном документе строку 11 удалить! Все вводимые данные должны быть string!
        // Без данного класса выводится целый элемент с БД в формате:
        /*
        <doctor><id>1</id><name>Имя</name><birthday>01.01.1970</birthday><experience>10</experience><animals-treated>100</animals-treated><telegram-id>@telegram</telegram-id></doctor>
        */
        public string id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
        public string experience { get; set; }
        public string animalsTreated { get; set; }
        public string telegramId { get; set; }

        // Конструктор класса
        public Doctors(string id, string name, string birthday, string experience, string animalsTreated, string telegramId)
        {
            // Присвоение значений
            this.id = id;
            this.name = name;
            this.birthday = birthday;
            this.experience = experience;
            this.animalsTreated = animalsTreated;
            this.telegramId = telegramId;
        }
        public Doctors() 
        {
            // 
            this.id = "0";
            this.name = "Неизвестное Имя";
            this.birthday = "Неизвестная дата рождения";
            this.experience = "Неопределённый стаж работы";
            this.animalsTreated = "Неопределённое кол-во проделанной работы";
            this.telegramId = "TG: Айди не задан";
        }
    }
}

