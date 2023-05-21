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
    public class XmlChange
    {
        // Обновить данные по ID
        public static void Update(string FileName, string MainTag, string ObjectTag, int id)
        {
            Title.Set($"{Lang.GetText("title_change", FileName, id)}");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/{FileName}.xml");
            XmlNode ?FileNameNode = document.SelectSingleNode($"/{MainTag}/{ObjectTag}" + "[id='" + id + "']");

            if (FileNameNode != null)
            {
                switch (FileName)
                {
                    case "doctors":
                        string? fullName, birthday, animalsTreated, telegramID;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("change_doctor_id", id)}.");
                            Title.Wait();

                            fullName = Valid.FullNameUser($"{Lang.GetText("add_doctor_fullname")}");
                            birthday = Valid.Date($"{Lang.GetText("add_doctor_date")}");
                            animalsTreated = Valid.Number($"{Lang.GetText("add_doctor_clients")}: ");
                            telegramID = Valid.TelegramID($"{Lang.GetText("add_doctor_telegram")}: ");

                        } while (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(birthday) || string.IsNullOrWhiteSpace(animalsTreated) || string.IsNullOrWhiteSpace(telegramID));
                        
                        FileNameNode.SelectSingleNode("fullname-doctor").InnerText = fullName;
                        FileNameNode.SelectSingleNode("birthday").InnerText = birthday;
                        FileNameNode.SelectSingleNode("animals-treated").InnerText = animalsTreated;
                        FileNameNode.SelectSingleNode("telegram-id").InnerText = telegramID;

                        document.Save("./database/doctors.xml");
                        Console.Clear();
                        Title.Set($"{Lang.GetText("title_success")}");
                        Console.WriteLine($"{Lang.GetText("change_doctor_success", id)}Доктор изменён в базе данных под ID ({id})");
                        Title.Wait();
                        break;
                    case "admission":
                        string? pacienteId, dateTime, fullnameDoctor, complaints, diagnosis, time;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("change_admission_id", id)}.");
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
                                    string answer = Console.ReadLine();
                                    if (answer.ToLower() != "д" || answer.ToLower() != "да"  || answer.ToLower() != "yes"  || answer.ToLower() != "y")
                                        return;
                                }
                            }
                            dateTime = Valid.Date($"{Lang.GetText("add_admission_date")}");

                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("add_admission_time")}: ");
                            time = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(time))
                                time = "00:00";

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
                                    Console.Write($"{Lang.GetText("add_error_doctor_id")} \n{Lang.GetText("string_try_again")}\n{Lang.GetText("string_input")}: ");
                                    string answer = Console.ReadLine();
                                    if (answer.ToLower() != "д" || answer.ToLower() != "да"  || answer.ToLower() != "yes"  || answer.ToLower() != "y")
                                        return;
                                }
                            }
                            
                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_complaints")}: ");
                            complaints = Console.ReadLine();

                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_diagnosis")}: ");
                            diagnosis = Console.ReadLine();
                            
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("change_admission_menu")}");
                            Title.Wait();
                        } while (string.IsNullOrWhiteSpace(pacienteId) || string.IsNullOrWhiteSpace(dateTime) || string.IsNullOrWhiteSpace(fullnameDoctor) || string.IsNullOrWhiteSpace(complaints) || string.IsNullOrWhiteSpace(diagnosis));
                        
                        FileNameNode.SelectSingleNode("paciente-id").InnerText = pacienteId;
                        FileNameNode.SelectSingleNode("date-time").InnerText = dateTime;
                        FileNameNode.SelectSingleNode("fullname-doctor").InnerText = fullnameDoctor;
                        FileNameNode.SelectSingleNode("complaints").InnerText = complaints;
                        FileNameNode.SelectSingleNode("diagnosis").InnerText = diagnosis;
                        FileNameNode.SelectSingleNode("time").InnerText = time;

                        document.Save("./database/admission.xml");
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("change_admission_success", id)}");
                        Title.Set($"{Lang.GetText("title_success")}");
                        Title.Wait();
                        break;
                    case "cassa":
                        string? idAdmission, amount;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("change_lot_id", id)}.");
                            Title.Wait();

                            while (true)
                            {
                                idAdmission = Valid.Number($"{Lang.GetText("change_input_admission_id")}: ");
                                XDocument doc = XDocument.Load("./database/admission.xml");
                                // Поиск и просмотр элемента в БД по ID
                                bool idExists = doc.Descendants("id").Any(x => (string)x == idAdmission);

                                if (idExists)
                                    break;
                                else
                                {
                                    Console.Clear();
                                    Console.Write($"{Lang.GetText("add_error_lot_id")}. \n{Lang.GetText("string_try_again")}\n{Lang.GetText("string_input")}: ");
                                    string answer = Console.ReadLine();
                                    if (answer.ToLower() != "д" || answer.ToLower() != "да"  || answer.ToLower() != "yes"  || answer.ToLower() != "y")
                                        return;
                                }
                            }
                            amount = Valid.Number($"{Lang.GetText("change_input_admission_cost")}: ");
                        } while (string.IsNullOrWhiteSpace(idAdmission) || string.IsNullOrWhiteSpace(amount));
                        
                        FileNameNode.SelectSingleNode("id-admission").InnerText = idAdmission;
                        FileNameNode.SelectSingleNode("amount").InnerText = amount;

                        document.Save("./database/cassa.xml");
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("change_lot_success", id)}");
                        Title.Set($"{Lang.GetText("title_success")}");
                        Title.Wait();
                        break;
                    case "pacientes":
                        string? name, age;
                        do 
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("change_patient_id", id)}.");
                            Title.Wait();

                            Console.Clear();
                            Console.Write($"{Lang.GetText("add_input_patient_name")}: ");
                            name = Console.ReadLine();

                            age = Valid.Number($"{Lang.GetText("add_input_patient_age")}: ");
                        } while (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(age));
                        
                        FileNameNode.SelectSingleNode("name").InnerText = name;
                        FileNameNode.SelectSingleNode("age").InnerText = age;

                        document.Save("./database/paciente.xml");
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("change_patient_success", id)}");
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
            else
            {
                Title.Set($"{Lang.GetText("title_error_simpl")}");
                Console.WriteLine($"{Lang.GetText("change_error_id")}.");
                Title.Wait();
            }
        }
        
        // Отдельное обновление информации у Приёмов, ибо информация требует больше текста
        public static void UpdateInfo(string id) 
        {
            Title.Set($"{Lang.GetText("title_change_admission", id)}");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/admission.xml");
            XmlNode admissionNode = document.SelectSingleNode($"/admission/animal[id='{id}']");

            if (admissionNode != null)
            {
                string? info;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"{Lang.GetText("change_admission_id", id)}");

                    Console.WriteLine($"{Lang.GetText("change_input_admission_info_0")}\n{Lang.GetText("change_input_admission_info_1")}.");
                    Console.WriteLine($"{Lang.GetText("line")}\n{Lang.GetText("string_prev_info")}:\n" + admissionNode.SelectSingleNode("info").InnerText + $"\n{Lang.GetText("line")}");
                    Console.Write($"{Lang.GetText("string_start_input")}: ");
                    info = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(info));
                admissionNode.SelectSingleNode("info").InnerText = info;
                document.Save("./database/admission.xml");
                Console.Clear();
                Console.WriteLine($"{Lang.GetText("change_admission_success", id)}");
                Title.Set($"{Lang.GetText("title_success")}");
                Title.Wait();
            }
            else
            {
                Console.Clear();
                Title.Set($"{Lang.GetText("string_nothing_found")}");
                Console.WriteLine($"{Lang.GetText("change_error_admission_id")}\n{Lang.GetText("string_try_again")}\n{Lang.GetText("string_input")}");
                Title.Wait();
            }
        }
        public static void UpdateValid(string id) 
        {
            Title.Set($"{Lang.GetText("title_change_pacientes", id)}");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/pacientes.xml");
            XmlNode ?admissionNode = document.SelectSingleNode($"/pacientes/paciente[id='{id}']");

            if (admissionNode != null)
            {
                string? valid, check, result;
                valid = "";
                result = "";
                check = admissionNode.SelectSingleNode("valid").InnerText;
                
                Console.Clear();
                Console.WriteLine($"{Lang.GetText("change_patoent_id", id)}");

                Console.WriteLine($"{Lang.GetText("change_patient_valid")}: " + check);

                Console.WriteLine($"{Lang.GetText("change_valid")}: ");
                switch (Choice.Get())
                {
                    case 1:
                        if (check == "Действительная")
                        {
                            result = "Не действительная";
                            valid = "Нет";
                        }
                        else
                        {
                            result = "Действительная";
                            valid = "Да";
                        }
                        break;
                    default:
                        Console.Clear();
                        Title.Set($"{Lang.GetText("string_cancel")}");
                        Console.WriteLine($"{Lang.GetText("change_valid_cancel")}");
                        Title.Wait();
                        return;
                }
                XmlDocument doctor = new XmlDocument();
                doctor.Load($"./database/doctors.xml");
                XmlNode ?doctorNode = document.SelectSingleNode($"/doctors/doctor[fullname-doctor='{admissionNode.SelectSingleNode("fullname-doctor").InnerText}']");
                if (doctorNode != null)
                {
                    if (valid.ToLower() == "да")
                    {
                        valid = "Да";
                        doctorNode.SelectSingleNode("animals-treated").InnerText = (Convert.ToInt32(doctorNode.SelectSingleNode("animals-treated").InnerText) + 1).ToString();
                    }
                    else if (valid.ToLower() == "нет")
                    {
                        valid = "Нет";
                    }
                }

                admissionNode.SelectSingleNode("valid").InnerText = result;
                document.Save("./database/pacientes.xml");
                doctor.Save("./database/doctors.xml");
                Console.Clear();
                Console.WriteLine($"{Lang.GetText("change_admission_success", id)}");
                Title.Set($"{Lang.GetText("title_success")}");
                Title.Wait();
            }
        }

        public static void UpdatePaid(string id) 
        {
            Title.Set($"{Lang.GetText("title_change_cassa", id)}");
            XmlDocument document = new XmlDocument();
            document.Load($"./database/cassa.xml");
            XmlNode admissionNode = document.SelectSingleNode($"./cassa/payment[id='{id}']");

            if (admissionNode != null)
            {
                string? valid, check, result;
                valid = "";
                result = "";
                check = admissionNode.SelectSingleNode("valid").InnerText;
                
                Console.Clear();
                Console.WriteLine($"{Lang.GetText("change_lot_id", id)}");

                Console.WriteLine($"{Lang.GetText("change_lot_menu_0")}: " + check);

                Console.WriteLine($"{Lang.GetText("change_status")}: ");
                switch (Choice.Get())
                {
                    case 1:
                        if (check == "Оплачен")
                        {
                            result = "Не оплачен";
                        }
                        else
                        {
                            result = "Оплачен";
                        }
                        break;
                    default:
                        Console.Clear();
                        Title.Set($"{Lang.GetText("string_cancel")}");
                        Console.WriteLine($"{Lang.GetText("change_status_cancel")}");
                        Title.Wait();
                        return;
                }

                admissionNode.SelectSingleNode("status").InnerText = result;
                document.Save("./database/cassa.xml");
                Console.Clear();
                Console.WriteLine($"{Lang.GetText("change_status_success", result, id)}");
                Title.Set($"{Lang.GetText("title_success")}");
                Title.Wait();
            }
        }
    }
}