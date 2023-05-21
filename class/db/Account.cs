using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
using System.Globalization;

namespace VeterinaryClinicRB
{
    public partial class Account
    {
        public static bool UserExists(string fullName)
        {
            XDocument xDoc = XDocument.Load("./database/accounts.xml");
            var user = xDoc.Descendants("User").FirstOrDefault(u => u.Attribute("fullname-user").Value == fullName);
            return user != null;
        }

        public static string GetFullNameByLogin(string login)
        {
            XDocument xDoc = XDocument.Load("./database/accounts.xml");
            var user = xDoc.Descendants("User").FirstOrDefault(u => u.Attribute("login").Value == login);
            if (user != null)
            {
                return user.Attribute("fullname-user").Value;
            }
            else
            {
                return "-";
            }
        }

        public static void PrintUserInfo(string? login)
        {
            if (login == null || string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login))
            {
                Console.Clear();
                Title.Set(Lang.GetText("title_error_search"));
                Console.WriteLine("Стоит ввести данные, чтобы продолжить.");
                Title.Wait();
                return;
            }

            XDocument xDoc = XDocument.Load("./database/accounts.xml");
            var user = xDoc.Descendants("User").FirstOrDefault(u => u.Attribute("login").Value == login);

            Console.Clear();
            if (user != null)
            {
                string password = "";
                if (Authorization.nowLogin == "ADMIN")
                    if (login != "ADMIN")
                    {
                        password = $"{Decrypt.Get(user.Attribute("password").Value, 3)} (Разблокировано)";
                        password += $"\nP.S: Вы администратор и имеете право просмотреть пароли уч. записей!";
                    }

                if (password == "")
                    password = $"{user.Attribute("password").Value} (Зашифрован!)";
                
                Console.WriteLine(
                    $"{Lang.GetText("info_doctor", user.Attribute("doctor-id").Value)}\n" +
                    $"{Lang.GetText("login", user.Attribute("login").Value)}\n" +
                    $"{Lang.GetText("full", user.Attribute("fullname-user").Value)}\n" +
                    $"{Lang.GetText("passvord", password)}"
                    );
            }
            else
                Console.WriteLine($"{Lang.GetText("user_not_found")}");
            Title.Wait();
        }

        public static void PrintUserLogins(int pageNum = 1)
        {
            int pageSize = 5; // Количество логинов на страницу
            XDocument xDoc = XDocument.Load("./database/accounts.xml");
            var users = xDoc.Descendants("User").Select(u => u.Attribute("login").Value).ToList();
            int pageCount = (int)Math.Ceiling((double)users.Count / pageSize);

            if (pageNum < 1 || pageNum > pageCount)
            {   
                Console.Clear();
                Title.Set($"{Lang.GetText("page_not_valid")}");
                Console.WriteLine($"{Lang.GetText("page_not_exist", pageNum)}");
                Title.Wait();
            }

            int startIndex = (pageNum - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, users.Count - 1);

            Console.Clear();
            Title.Set($"{Lang.GetText("page_login", pageNum, pageCount)}");

            for (int i = startIndex; i <= endIndex; i++)
            {
                Console.WriteLine($"{i + 1 - startIndex}. {users[i]}");
            }
            
            Console.Write(
                $"{Lang.GetText("page", pageNum, pageCount)}.\n" +
                $"{Lang.GetText("input_user_number")}\n" +
                $"{Lang.GetText("string_input")}: "
                );
            
            int userNum;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.N && pageNum < pageCount)
                {
                    PrintUserLogins(pageNum: pageNum + 1);
                    return;
                }
                else if (keyInfo.Key == ConsoleKey.P && pageNum < 1)
                {
                    PrintUserLogins(pageNum: pageNum - 1);
                    return;
                }
                else if (int.TryParse(keyInfo.KeyChar.ToString(), out userNum))
                {
                    if (userNum >= 1 && userNum <= pageSize)
                        PrintUserInfo(users[startIndex + userNum - 1]);
                    else if (userNum != 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"{Lang.GetText("incoorrect_input_1", pageSize)}.\n{Lang.GetText("string_input")}: ");
                        Title.Wait();
                    }
                }
                else if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    Console.Clear();
                    Console.WriteLine($"{Lang.GetText("incoorrect_input", pageSize)}.\n{Lang.GetText("string_input")}: ");
                    Title.Wait();
                }
            } while (userNum != 0);
        }

        public static void ChangeUserData(string filePath)
        {
            // загружаем XML-файл
            XDocument doc = XDocument.Load(filePath);
            string login;
            
            if (Authorization.nowLogin == "ADMIN")
            {
                Console.Clear();
                Console.Write($"{Lang.GetText("login_input")}: ");
                login = Console.ReadLine();
            }
            else
                login = Authorization.nowLogin;

            // ищем пользователя по логину
            XElement user = doc.Descendants("User").Where(x => (string)x.Attribute("login") == login).FirstOrDefault();
            XElement admin = doc.Descendants("User").Where(x => (string)x.Attribute("login") == "ADMIN").FirstOrDefault();

            if (user == null)
            {
                Console.Clear();
                Console.WriteLine(Lang.GetText("user_not_found"));
                Title.Wait();
                return;
            }

            Console.Write($"{Lang.GetText("login_password")}: ");
            string password = Console.ReadLine();

            Console.Clear();
            if (Decrypt.Get((string)user.Attribute("password"), 3) != password && Decrypt.Get((string)admin.Attribute("password"), 3) != password)
            {
                Console.WriteLine(Lang.GetText("login_password_error"));
                Title.Wait();
                return;
            }

            Console.WriteLine($"{Lang.GetText("string_choise")}:");
            string choice1 = $"1. {Lang.GetText("choice_change_acc_0")}";
            if (login == "ADMIN")
                choice1 += $" ({Lang.GetText("no_permission")})";
            Console.WriteLine(choice1);
            Console.Write($"2. {Lang.GetText("choice_change_acc_1")}\n{Lang.GetText("string_input")}: ");
            int choice;

            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine(Lang.GetText("string_error_input"));
                Title.Wait();
                return;
            }
            
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.Write($"{Lang.GetText("input_new_password")}:");
                    string newLogin = Console.ReadLine();

                    // проверяем, что новый логин не повторяется
                    if (doc.Descendants("User").Any(x => (string)x.Attribute("login") == newLogin) || login == "ADMIN")
                    {   
                        Console.Clear();
                        if (login != "ADMIN")
                            Console.WriteLine(Lang.GetText("exit_account"));
                        else
                            Console.WriteLine($"{Lang.GetText("account_admin_security")}");
                        Title.Wait();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(newLogin))
                    {
                        Console.Clear();
                        Console.WriteLine(Lang.GetText("reg_error_login"));
                        Title.Wait();
                        return;
                    }

                    user.Attribute("login").Value = newLogin;
                    break;

                case 2:
                    Console.Write($"{Lang.GetText("password_new")}: ");
                    string newPassword = Console.ReadLine();
                    Console.Write($"{Lang.GetText("password_confirm_new")}: ");
                    string confirmPassword = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newPassword))
                    {
                        Console.Clear();
                        Console.WriteLine(Lang.GetText("password_null"));
                        Title.Wait();
                        return;
                    }

                    if (newPassword != confirmPassword)
                    {
                        Console.Clear();
                        Console.WriteLine(Lang.GetText("reg_error_pass_confirm"));
                        Title.Wait();
                        return;
                    }

                    user.Attribute("password").Value = Encrypt.Get(newPassword, 3);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine(Lang.GetText("title_error"));
                    Title.Wait();
                    return;
            }

            doc.Save(filePath); // сохраняем измененный XML-файл
            Console.Clear();
            Console.WriteLine(Lang.GetText("done_woek_save_account"));
            Title.Wait();
        }
    }
}