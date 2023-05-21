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
        public static void New(string FileName, string MainTag, string ObjectTag)
        {
            Title.Set($"{Lang.GetText("title_add_element",FileName)}");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            
            XmlNode root = document.SelectSingleNode($"/{MainTag}");
            XmlElement AddElement = document.CreateElement($"{ObjectTag}");

            switch (FileName)
                {
                    case "doctors":
                        string? id, fullName, birthday, animalsTreated, telegramID;
                        id = XmlRead.GetFreeID(FileName).ToString();
                        do 
                        {
                            Title.Set($"{Lang.GetText("title_add_doctor_id", id)}");
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("add_doctor_menu",id)}");
                            Title.Wait();

                            fullName = Valid.FullNameUser($"{Lang.GetText("add_doctor_fullname")}");
                            birthday = Valid.Date($"{Lang.GetText("add_doctor_date")}");
                            telegramID = Valid.TelegramID($"{Lang.GetText("add_doctor_telegram")}: ");

                        } while (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(birthday) || string.IsNullOrWhiteSpace(telegramID));

                        XmlElement idElement = document.CreateElement("id");
                        XmlElement fullNameElement = document.CreateElement("fullname-doctor");
                        XmlElement birthdayElement = document.CreateElement("birthday");
                        XmlElement animalsTreatedElement = document.CreateElement("animals-treated");
                        XmlElement telegramIDElement = document.CreateElement("telegram-id");

                        idElement.InnerText = id;
                        fullNameElement.InnerText = fullName;
                        birthdayElement.InnerText = birthday;
                        animalsTreatedElement.InnerText = "0";
                        telegramIDElement.InnerText = telegramID;

                        AddElement.AppendChild(idElement);
                        AddElement.AppendChild(fullNameElement);
                        AddElement.AppendChild(birthdayElement);
                        AddElement.AppendChild(animalsTreatedElement);
                        AddElement.AppendChild(telegramIDElement);

                        root?.AppendChild(AddElement);
                        document.Save($"./database/{FileName}.xml");

                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("add_doctor_success",id)}");
                        Title.Set($"{Lang.GetText("title_success")}");
                        Title.Wait();
                        break;
                    case "admission":
                        string? pacienteId, time, dateTime, fullnameDoctor, complaints, diagnosis, info;
                        id = XmlRead.GetFreeID(FileName).ToString();
                        XmlDocument doctor = new XmlDocument();
                        document.Load($"./database/doctors.xml");
                        Title.Set($"{Lang.GetText("add_admission_menu", id)}");
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("add_admission_new_id", id)}");
                            Title.Wait();
                            
                            while (true)
                            {
                                pacienteId = Valid.Number($"{Lang.GetText("add_input_patient_id")}: ");
                                XDocument doc = XDocument.Load("./database/pacientes.xml");
                                // Поиск и просмотр элемента в БД по ID
                                bool idExists = doc.Descendants("id").Any(x => (string)x == pacienteId);

                                if (idExists)
                                    break;
                                else
                                {
                                    Console.Clear();
                                    Console.Write($"{Lang.GetText("add_error_pacient_id")} \n{Lang.GetText("string_try_again")}\n{Lang.GetText("string_input")}: ");
                                    string? answer = Console.ReadLine();
                                    if (answer.ToLower() != "д" || answer.ToLower() != "да" || answer.ToLower() != "y" || answer.ToLower() != "yes")
                                        return;
                                }
                            }

                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("add_admission_time")}: ");
                            time = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(time))
                                time = "00:00";

                            dateTime = Valid.Date($"{Lang.GetText("add_admission_date")}");
                            
                            if (Authorization.nowLogin == "ADMIN")
                            {
                                while (true)
                                {
                                    fullnameDoctor = Valid.FullNameUser($"{Lang.GetText("add_doctor_fullname")}");
                                    XDocument doc = XDocument.Load("./database/doctors.xml");
                                    // Поиск и просмотр элемента в БД по ID
                                    bool idExists = doc.Descendants("fullname-doctor").Any(x => (string)x == fullnameDoctor);

                                    if (idExists)
                                        break;
                                    else
                                    {
                                        Console.Clear();
                                        Console.Write($"{Lang.GetText("add_error_doctor_id")}\n{Lang.GetText("string_try_again")}\n{Lang.GetText("string_input")}: ");
                                        string? answer = Console.ReadLine();
                                        if (answer.ToLower() != "д" || answer.ToLower() != "да" || answer.ToLower() != "y" || answer.ToLower() != "yes")
                                            return;
                                    }
                                }
                            }
                            else
                            {
                                XDocument doc = XDocument.Load("./database/doctors.xml");
                                XDocument acc = XDocument.Load("./database/accounts.xml");
                                fullnameDoctor = "";
                                string accLog = Account.GetFullNameByLogin(Authorization.nowLogin);
                                
                                // Получить ФИО по логину, который авторизирован
                                if (accLog != "-")
                                {

                                    bool idExists = doc.Descendants("fullname-doctor").Any(x => (string)x == accLog);
                                    
                                    Console.Clear();
                                    if (idExists)
                                    {
                                        Console.WriteLine($"{Lang.GetText("add_addmission_error", accLog)}");
                                        fullnameDoctor = accLog;
                                        Title.Wait();
                                    }
                                    else
                                    {
                                        Title.Set("Ошибка доступа");
                                        Console.WriteLine(
                                            $"{Lang.GetText("add_admission_error_account_0")}\n" +
                                            $"{Lang.GetText("add_admission_error_account_1")}"
                                        );
                                        Title.Wait();
                                        return;
                                    }
                                }
                            }
                            
                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_complaints")}: ");
                            complaints = Console.ReadLine();

                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_diagnosis")}: ");
                            diagnosis = Console.ReadLine();

                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_additional_info")}: ");
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
                        document.Save($"./database/{FileName}.xml");
                        
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("add_admission_success", id)}");
                        Title.Set($"{Lang.GetText("title_success")}");
                        Title.Wait();
                        break;
                    case "cassa":
                        string idAdmission, amount;
                        id = XmlRead.GetFreeID(FileName).ToString();
                        XmlDocument cassa = new XmlDocument();
                        document.Load($"./database/cassa.xml");
                        Title.Set($"{Lang.GetText("title_new_lot", id)}");
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("add_new_lot_id", id)}");
                            Title.Wait();
                            XDocument doc = XDocument.Load("./database/admission.xml");
                            
                            while (true)
                            {
                                idAdmission = Valid.Number($"{Lang.GetText("add_input_admission_id")}: ");
                                // Поиск и просмотр элемента в БД по ID
                                bool idExists = doc.Descendants("id").Any(x => (string)x == idAdmission);

                                if (idExists)
                                    break;
                                else
                                {
                                    Console.Clear();
                                    Console.Write($"{Lang.GetText("add_error_admission_id")} \n{Lang.GetText("string_try_again")}\n{Lang.GetText("string_input")}: ");
                                    string? answer = Console.ReadLine();
                                    if (answer.ToLower() != "д" || answer.ToLower() != "да" || answer.ToLower() != "y" || answer.ToLower() != "yes")
                                        return;
                                }
                            }

                            amount = Valid.Number($"{Lang.GetText("add_lot_cost")}: ");
                            
                        } while (string.IsNullOrWhiteSpace(idAdmission) || string.IsNullOrWhiteSpace(amount));

                        Element1 = document.CreateElement("id");
                        Element2 = document.CreateElement("id-admission");
                        Element3 = document.CreateElement("amount");
                        Element4 = document.CreateElement("date");
                        Element5 = document.CreateElement("status");

                        Element1.InnerText = id;
                        Element2.InnerText = idAdmission;
                        Element3.InnerText = $"{amount}";
                        Element4.InnerText = DateTime.Today.ToString("dd.MM.yyyy");
                        Element5.InnerText = "Не оплачен";

                        AddElement.AppendChild(Element1);
                        AddElement.AppendChild(Element2);
                        AddElement.AppendChild(Element3);
                        AddElement.AppendChild(Element4);
                        AddElement.AppendChild(Element5);


                        root.AppendChild(AddElement);
                        document.Save($"./database/cassa.xml");
                        
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("add_lot_success", id)}");
                        Title.Set($"{Lang.GetText("title_success")}");
                        Title.Wait();
                        break;
                    case "pacientes":
                        string? animalType, name, gender, age, fullnameOwner, valid;
                        id = XmlRead.GetFreeID(FileName).ToString();
                        do 
                        {
                            Title.Set($"{Lang.GetText("title_new_patient", id)}");
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("add_new_pacient_id", id)}");
                            Title.Wait();
                            
                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_patient_type")}: ");
                            animalType = Console.ReadLine();

                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_patient_name")}: ");
                            name = Console.ReadLine();
                            
                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_patient_sex")}: ");
                            gender = Console.ReadLine();

                            age = Valid.Number($"{Lang.GetText("add_input_patient_age")}: ");
                            fullnameOwner = Valid.FullNameUser($"{Lang.GetText("add_input_patient_owner")}");
                            telegramID = Valid.TelegramID($"{Lang.GetText("add_input_owner_telegram")}: ");
                        } while (string.IsNullOrWhiteSpace(animalType) || string.IsNullOrWhiteSpace(name)  || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(fullnameOwner) || string.IsNullOrWhiteSpace(telegramID));

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
                        Element7.InnerText = "Да";
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
                        document.Save($"./database/{FileName}.xml");
                        
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("add_patient_success", id)}");
                        Title.Set($"{Lang.GetText("title_success")}");
                        Title.Wait();
                        break;
                    default:
                        Title.Set($"{Lang.GetText("title_error_simpl")}");
                        Console.WriteLine($"{Lang.GetText("string_wrong_file")}");
                        Title.Wait();
                        break;
                }
        }

        public static void AddStatistic()
        {
            Title.Set($"{Lang.GetText("title_event_menu_0")}");
            XmlDocument doc = new XmlDocument();
            doc.Load("./database/statistic.xml");

            // Создание нового элемента
            XmlElement newElement = doc.CreateElement("data");
            XmlElement dateElement = doc.CreateElement("date");
            XmlElement descriptionElement = doc.CreateElement("description");
            
            string? date, description;
            do 
            {
                Console.Clear();
                Console.WriteLine($"{Lang.GetText("add_new_event")}");

                Console.Write($"{Lang.GetText("add_input_event_date")}: ");
                date = Console.ReadLine();

                Console.Write($"{Lang.GetText("add_input_event_description")}: ");
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

            Title.Set($"{Lang.GetText("title_success")}Успех!");
            Title.Wait();
            // Сохранение изменений в файл
            doc.Save("./database/statistic.xml");
        }
    }
}