using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace VeterinaryClinicRB
{
    public partial class Authorization
    {
        // Добавить пользователя
        public static void AddUser(string login, string password, string fullnameUser, string doctorID)
        {
            XmlDocument accountDoc = new XmlDocument();
            accountDoc.Load("./database/accounts.xml");

            XmlElement accountRoot = accountDoc.DocumentElement;

            XmlElement user = accountDoc.CreateElement("User");
            user.SetAttribute("login", login);
            user.SetAttribute("doctor-id", doctorID);
            user.SetAttribute("fullname-user", fullnameUser);
            user.SetAttribute("password", Encrypt.Get(password, 3));

            accountRoot.AppendChild(user);
            accountDoc.Save("./database/accounts.xml");
        }

        // Проверка, есть ли пользователь в бд
        public static bool CheckUserExists(string login)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("./database/accounts.xml");

            XmlNodeList users = doc.SelectNodes("//User");

            foreach (XmlNode user in users)
                if (user.Attributes["login"].Value == login)
                    return true;

            return false;
        }

        public static void Register()
        {
            string? doctorID = "";
            string? login = "";
            string? password = "";
            string? fullnameDoctor = "";
            
            Console.Clear();
            Title.Set(Lang.GetText("title_account_register"));
            fullnameDoctor = Valid.FullNameUser(Lang.GetText("valid_full_name"));

            if (!string.IsNullOrEmpty(fullnameDoctor))
            {
                Console.Clear();
                doctorID = Valid.Number(Lang.GetText("reg_input_doctor_id") + "\n" + Lang.GetText("string_input"));
                if (doctorID == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("./database/doctors.xml");
                    Console.Clear();
                    XmlNode doctor = xmlDoc.SelectSingleNode($"//doctor[fullname-doctor='{fullnameDoctor}']");
                    if (doctor != null)
                    {
                        doctorID = doctor.SelectSingleNode("id").InnerText;
                        Console.WriteLine(Lang.GetText("reg_search_doctor_id_done", doctorID));
                    }
                    else
                    {
                        Console.WriteLine(Lang.GetText("reg_search_doctor_id_error"));
                        Title.Wait();
                        return;
                    }
                    Title.Wait();
                }

                if (!string.IsNullOrEmpty(doctorID))
                {
                    if (!DBNull.CheckIfDoctorHasAccounts(doctorID))
                    {
                        Console.Clear();
                        Console.Write(Lang.GetText("reg_input_login") + ": ");
                        login = Console.ReadLine();

                        if (!string.IsNullOrEmpty(login))
                        {
                            Console.Clear();
                            Console.Write(Lang.GetText("reg_input_pass") + ": ");
                            password = Console.ReadLine();

                            if (!string.IsNullOrEmpty(password))
                            {
                                Console.Clear();
                                Console.Write(Lang.GetText("reg_input_pass_confirm") + ": ");
                                string? confirmPassword = Console.ReadLine();

                                Console.Clear();
                                if (confirmPassword == password)
                                {
                                    AddUser(login, password, fullnameDoctor, doctorID);
                                    Console.WriteLine(Lang.GetText("reg_done", login));
                                }
                                else
                                {
                                    Console.WriteLine(Lang.GetText("reg_error_pass_confirm"));
                                }
                                Title.Wait();
                            }
                            else 
                            {
                                Console.Clear();
                                Console.WriteLine(Lang.GetText("reg_error_pass"));
                                Title.Wait();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(Lang.GetText("reg_error_login"));
                            Title.Wait();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(Lang.GetText("reg_error_account_alredy_exits"));
                        Title.Wait();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(Lang.GetText("reg_error_doctor_id"));
                    Title.Wait();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(Lang.GetText("reg_error_bio"));
                Title.Wait();
            }
            return;
        }

        public static string? ValidateUser(bool autoLogin)
        {
            string? login = "";
            Title.Set(Lang.GetText("title_validate_user"));
            Console.Clear();
            if (autoLogin) // Есть ли логин в конфигах
            {
                login = Config.Get("Login");
                if (login == "Данный ключ отсутствует  ")
                {
                    login = "";
                    Config.Set("AutoLogin", false);
                    Console.Clear();
                }
            }

            if (login == "")
            {
                Console.Clear();
                Console.Write($"{Lang.GetText("login_input")}: ");
                login = Console.ReadLine();
            }
            
            if (!string.IsNullOrEmpty(login))
            {
                Console.Clear();
                Console.WriteLine(Lang.GetText("login_login", login));
                Console.Write($"{Lang.GetText("login_password")}: ");
                string? password = Console.ReadLine();

                XmlDocument accountDoc = new XmlDocument();
                accountDoc.Load("./database/accounts.xml");

                XmlNodeList users = accountDoc.SelectNodes("//User");
                
                string? valid = null;

                foreach (XmlNode user in users)
                {
                    if (user.Attributes["login"].Value == login && Decrypt.Get(user.Attributes["password"].Value, 3) == password)
                    {
                        valid = user.Attributes["fullname-user"].InnerText;
                    }
                }

                Console.Clear();
                if (valid != null)
                {
                    Console.WriteLine(Lang.GetText("login_done"));
                    Config.Set("Login", login);
                    Config.Set("AutoLogin", false);
                    Title.Wait();
                    return null;
                }
                else
                {
                    Console.WriteLine(Lang.GetText("login_password_error"));
                    Config.Set("Login", "");
                    Config.Set("AutoLogin", false);
                }
                Title.Wait();
                return "";
            }
            else
            {
                Console.Clear();
                Console.WriteLine(Lang.GetText("login_input_error"));
                Title.Wait();
            }
            
            return "";
        }
    }
}