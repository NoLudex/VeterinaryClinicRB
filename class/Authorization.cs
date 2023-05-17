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
            Title.Set("Регистрация");
            fullnameDoctor = Valid.FullNameUser("врача");

            if (!string.IsNullOrEmpty(fullnameDoctor))
            {
                Console.Clear();
                doctorID = Valid.Number("Введи ID врача, если незнаете, напишите 0");
                if (doctorID == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("./database/doctors.xml");
                    Console.Clear();
                    XmlNode doctor = xmlDoc.SelectSingleNode($"//doctor[fullname-doctor='{fullnameDoctor}']");
                    if (doctor != null)
                    {
                        doctorID = doctor.SelectSingleNode("id").InnerText;
                        Console.WriteLine($"Найден доктор под данным ФИО, его ID - {doctorID}");
                    }
                    else
                    {
                        Console.WriteLine("ФИО данного врача нет в базе данных. Возможно вы ошиблись");
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
                        Console.Write("Введите желаемый логин: ");
                        login = Console.ReadLine();

                        if (!string.IsNullOrEmpty(login))
                        {
                            Console.Clear();
                            Console.Write("Введите желаемый пароль: ");
                            password = Console.ReadLine();

                            if (!string.IsNullOrEmpty(password))
                            {
                                Console.Clear();
                                Console.Write("Повторите пароль: ");
                                string? confirmPassword = Console.ReadLine();

                                Console.Clear();
                                if (confirmPassword == password)
                                {
                                    AddUser(login, password, fullnameDoctor, doctorID);
                                    Console.WriteLine($"Вы успешно зарегистрировались! Login: {login}\nТеперь авторизуйтесь под данной учетной записью!");
                                }
                                else
                                {
                                    Console.WriteLine("Пароли не совпадают, попробуйте снова");
                                }
                                Title.Wait();
                            }
                            else 
                            {
                                Console.Clear();
                                Console.WriteLine("Вы должны ввести пароль, чтобы продолжить.");
                                Title.Wait();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Логин не может быть пустым, повторите попытку.");
                            Title.Wait();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Данный врач уже зарегистрирован!");
                        Title.Wait();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы должны ввести ID врача, чтобы продолжить!");
                    Title.Wait();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Вы должны ввести ФИО врача. Оно не должно быть пустым");
                Title.Wait();
            }
            return;
        }

        public static string? ValidateUser(bool autoLogin)
        {
            string? login = "";
            Title.Set("Авторизация");
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
                Console.Write("Введите логин: ");
                login = Console.ReadLine();
            }
            
            if (!string.IsNullOrEmpty(login))
            {
                Console.Clear();
                Console.WriteLine($"Логин: {login}");
                Console.Write("Введите пароль: ");
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
                    Console.WriteLine("Вы успешно авторизировались!");
                    Config.Set("Login", login);
                    Config.Set("AutoLogin", false);
                    Title.Wait();
                    return null;
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль. Попробуйте снова");
                    Config.Set("Login", "");
                    Config.Set("AutoLogin", false);
                }
                Title.Wait();
                return "";
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Логин не может быть пустым! Попробуйте снова");
                Title.Wait();
            }
            
            return "";
        }
    }
}