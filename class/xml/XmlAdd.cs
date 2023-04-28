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
    public class XmlAdd
    {
        // Добавить новый элемент в БД
        static void Add(string FileName, string MainTag, string ObjectTag)
        {
            Title.Set($"Добавление нового элемента {FileName}.xml");
            XmlDocument document = new XmlDocument();
            document.Load($"./././database/{FileName}.xml");
            
            XmlNode ?root = document.SelectSingleNode($"/{MainTag}");
            XmlElement AddElement = document.CreateElement($"{ObjectTag}");

            switch (FileName)
                {
                    case "doctors":
                        string? id, fullName, birthday, experience, animalsTreated, telegramID;
                        id = XmlRead.GetMaxId(FileName).ToString();
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы добавляете нового врача, его ID будет: ({id})");

                            Console.Write("Введите ФИО врача: ");
                            fullName = Console.ReadLine();

                            Console.Write("Введите дату рождения (ДД.ММ.ГГГГ): ");
                            birthday = Console.ReadLine();

                            Console.Write("Введите стаж работы врача: ");
                            experience = Console.ReadLine();
                            
                            Console.Write("Введите количество пройденных клиентов у врача\nВвод: ");
                            animalsTreated = Console.ReadLine();

                            Console.Write("Введите Telegram врача: ");
                            telegramID = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(telegramID))
                                    telegramID = "@" + telegramID;
                                else if (!telegramID.StartsWith("@"))
                                    telegramID = "@" + telegramID;
                        } while (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(birthday) || string.IsNullOrWhiteSpace(experience) || string.IsNullOrWhiteSpace(animalsTreated) || string.IsNullOrWhiteSpace(telegramID));

                        XmlElement idElement = document.CreateElement("id");
                        XmlElement fullNameElement = document.CreateElement("fullname-doctor");
                        XmlElement birthdayElement = document.CreateElement("birthday");
                        XmlElement experienceElement = document.CreateElement("experience");
                        XmlElement animalsTreatedElement = document.CreateElement("animals-treated");
                        XmlElement telegramIDElement = document.CreateElement("telegram-id");

                        idElement.InnerText = id;
                        fullNameElement.InnerText = fullName;
                        birthdayElement.InnerText = birthday;
                        experienceElement.InnerText = experience;
                        animalsTreatedElement.InnerText = animalsTreated;
                        telegramIDElement.InnerText = telegramID;

                        AddElement.AppendChild(idElement);
                        AddElement.AppendChild(fullNameElement);
                        AddElement.AppendChild(birthdayElement);
                        AddElement.AppendChild(experienceElement);
                        AddElement.AppendChild(animalsTreatedElement);
                        AddElement.AppendChild(telegramIDElement);

                        root?.AppendChild(AddElement);
                        document.Save($"./././database/{FileName}.xml");
                        int Abuz = Convert.ToInt32(id) + 1;
                        Console.Clear();
                        Console.WriteLine($"Новый профиль врача был создан, его ID ({id})");
                        Title.Set("Успех!");
                        Title.Wait();
                        break;
                    case "admission":
                        string? pacienteId, dateTime, fullnameDoctor, complaints, diagnosis, info;
                        id = XmlRead.GetMaxId(FileName).ToString();
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы добавляете новый приём, его ID будет: ({id})");

                            Console.Write("Введите ID пациента: ");
                            pacienteId = Console.ReadLine();

                            Console.Write("Введите дату приёма (ДД.ММ.ГГГГ): ");
                            dateTime = Console.ReadLine();

                            Console.Write("Введите ФИО доктора: ");
                            fullnameDoctor = Console.ReadLine();
                            
                            Console.Write("Введите жалобы: ");
                            complaints = Console.ReadLine();

                            Console.Write("Введите диагноз: ");
                            diagnosis = Console.ReadLine();

                            Console.Write("Введите доп. информацию об приёме: ");
                            info = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(pacienteId) || string.IsNullOrWhiteSpace(dateTime)  || string.IsNullOrWhiteSpace(info) || string.IsNullOrWhiteSpace(fullnameDoctor) || string.IsNullOrWhiteSpace(complaints) || string.IsNullOrWhiteSpace(diagnosis));

                        XmlElement Element1 = document.CreateElement("id");
                        XmlElement Element2 = document.CreateElement("paciente-id");
                        XmlElement Element3 = document.CreateElement("date-time");
                        XmlElement Element4 = document.CreateElement("fullname-doctor");
                        XmlElement Element5 = document.CreateElement("complaints");
                        XmlElement Element6 = document.CreateElement("diagnosis");
                        XmlElement Element7 = document.CreateElement("info");

                        Element1.InnerText = id;
                        Element2.InnerText = pacienteId;
                        Element3.InnerText = dateTime;
                        Element4.InnerText = fullnameDoctor;
                        Element5.InnerText = complaints;
                        Element6.InnerText = diagnosis;
                        Element7.InnerText = info;

                        AddElement.AppendChild(Element1);
                        AddElement.AppendChild(Element2);
                        AddElement.AppendChild(Element3);
                        AddElement.AppendChild(Element4);
                        AddElement.AppendChild(Element5);
                        AddElement.AppendChild(Element6);
                        AddElement.AppendChild(Element7);


                        root?.AppendChild(AddElement);
                        document.Save($"./././database/{FileName}.xml");
                        Abuz = Convert.ToInt32(id) + 1;
                        Console.Clear();
                        Console.WriteLine($"Новые данные об приёме были созданы, ID ({id})");
                        Title.Set("Успех!");
                        Title.Wait();
                        break;
                    case "pacientes":
                        string? animalType, name, gender, age, fullnameOwner, valid;
                        id = XmlRead.GetMaxId(FileName).ToString();
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"Вы добавляете новый приём, его ID будет: ({id})");

                            Console.Write("Введите тип животного: ");
                            animalType = Console.ReadLine();

                            Console.Write("Введите кличку животного: ");
                            name = Console.ReadLine();

                            Console.Write("Введите ФИО доктора: ");
                            fullnameDoctor = Console.ReadLine();
                            
                            Console.Write("Введите пол животного: ");
                            gender = Console.ReadLine();

                            Console.Write("Введите возраст животного: ");
                            age = Console.ReadLine();

                            Console.Write("Введите ФИО владельца: ");
                            fullnameOwner = Console.ReadLine();

                            Console.Write("Валиден ли данный пациент? (Да / Нет): ");
                            do
                                valid = Console.ReadLine();
                            while (valid == null || valid.ToLower() != "да" || valid.ToLower() != "нет");

                            Console.Write("Введите Telegram ID владельца: ");
                            telegramID = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(telegramID))
                                telegramID = "@" + telegramID;
                            else if (!telegramID.StartsWith("@"))
                                telegramID = "@" + telegramID;
                        } while (string.IsNullOrWhiteSpace(animalType) || string.IsNullOrWhiteSpace(name)  || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(fullnameOwner) || string.IsNullOrWhiteSpace(valid) || string.IsNullOrWhiteSpace(telegramID));

                        Element1 = document.CreateElement("id");
                        Element2 = document.CreateElement("animal-type");
                        Element3 = document.CreateElement("name");
                        Element4 = document.CreateElement("gender");
                        Element5 = document.CreateElement("age");
                        Element6 = document.CreateElement("fullname-owner");
                        Element7 = document.CreateElement("valid");
                        XmlElement Element8 = document.CreateElement("telegram-id");

                        Element1.InnerText = id;
                        Element2.InnerText = animalType;
                        Element3.InnerText = name;
                        Element4.InnerText = gender;
                        Element5.InnerText = age;
                        Element6.InnerText = fullnameOwner;
                        Element7.InnerText = valid;
                        Element8.InnerText = telegramID;

                        AddElement.AppendChild(Element1);
                        AddElement.AppendChild(Element2);
                        AddElement.AppendChild(Element3);
                        AddElement.AppendChild(Element4);
                        AddElement.AppendChild(Element5);
                        AddElement.AppendChild(Element6);
                        AddElement.AppendChild(Element7);
                        AddElement.AppendChild(Element8);


                        root?.AppendChild(AddElement);
                        document.Save($"./././database/{FileName}.xml");
                        Abuz = Convert.ToInt32(id) + 1;
                        Console.Clear();
                        Console.WriteLine($"Новый профиль пациента был добавлен, его ID ({id})");
                        Title.Set("Успех!");
                        Title.Wait();
                        break;
                    // case "statistic":
                    //     string? date, description;
                    //     do 
                    //     {
                    //         Console.Clear();
                    //         Console.WriteLine($"Вы добавляете новое событие дня");

                    //         Console.Write("Напишите дату события: ");
                    //         date = Console.ReadLine();

                    //         Console.Write("Напишите что произошло (Описание): ");
                    //         description = Console.ReadLine();
                    //     } while (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(description));

                    //     Element1 = document.CreateElement("date");
                    //     Element2 = document.CreateElement("description");

                    //     Element1.InnerText = date;
                    //     Element2.InnerText = description;

                    //     AddElement.AppendChild(Element1);
                    //     AddElement.AppendChild(Element2);

                    //     root?.AppendChild(AddElement);
                    //     document.Save($"./././database/{FileName}.xml");
                    //     Console.Clear();
                    //     Console.WriteLine($"Новое событие было добавлено на число: {date}");
                    //     XmlSort.SortByDate("./././database/statistic.xml");
                    //     Title.Set("Успех!");
                    //     Title.Wait();
                    //     break;
                    default:
                        Title.Set("Ошибка");
                        Console.WriteLine("[DEBUG] Неверный файл");
                        Title.Wait();
                        break;
                }
        }

        static void AddStatistic()
        {
            Title.Set("Добавление нового события");
            XmlDocument doc = new XmlDocument();
            doc.Load("./././database/statistic.xml");

            // Создание нового элемента
            XmlElement newElement = doc.CreateElement("data");
            XmlElement dateElement = doc.CreateElement("date");
            XmlElement descriptionElement = doc.CreateElement("description");
            
            string? date, description;
            do 
            {
                Console.Clear();
                Console.WriteLine($"Вы добавляете новое событие дня");

                Console.Write("Напишите дату события: ");
                date = Console.ReadLine();

                Console.Write("Напишите что произошло (Описание): ");
                description = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(description));
            dateElement.InnerText = date; // ввод даты с клавиатуры
            descriptionElement.InnerText = description; // ввод описания с клавиатуры

            // Добавление новых элементов в созданный элемент
            newElement.AppendChild(dateElement);
            newElement.AppendChild(descriptionElement);

            // Добавление нового элемента в документ
            XmlNode rootNode = doc.DocumentElement;
            rootNode.AppendChild(newElement);

            Title.Set("Успех!");
            Title.Wait();
            // Сохранение изменений в файл
            doc.Save("./././database/statistic.xml");
        }
    }
}