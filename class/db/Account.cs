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
                        password = $"{Decrypt.Get(user.Attribute("password").Value, 3)} (ЗАЩИТА!)";

                if (password == "")
                    password = $"{user.Attribute("password").Value} (Зашифрован!)";
                
                Console.WriteLine(
                    $"Сведения об учетной записи врача с ID ({user.Attribute("doctor-id").Value})\n" +
                    $"Логин: {user.Attribute("login").Value}\n" +
                    $"Пароль: {password}\n" +
                    $"ФИО: {user.Attribute("fullname-user").Value}\n" +
                    $"ID врача: {user.Attribute("doctor-id").Value}"
                    );
            }
            else
                Console.WriteLine("Произошла ошибка, пользователь не найден!");
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
                Title.Set("Данной страницы не существует");
                Console.WriteLine($"Страницы с номером {pageNum} не существует!");
                Title.Wait();
            }

            int startIndex = (pageNum - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, users.Count - 1);

            Console.Clear();
            Title.Set("{pageNum} / {pageCount} | Логины:");

            for (int i = startIndex; i <= endIndex; i++)
            {
                Console.WriteLine($"{i + 1 - startIndex}. {users[i]}");
            }
            
            Console.Write(
                $"Страница {pageNum} / {pageCount}.\n" +
                $"Введите номер пользователя для просмотра информации или 0 для выхода\n" +
                $"Ввод: "
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
                        Console.Write($"Следует выбрать номер в диапазоне от 1 до {pageSize} или введите 0 для выхода.\nВвод: ");
                        Title.Wait();
                    }
                }
                else if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    Console.Clear();
                    Console.WriteLine($"Некорректный ввод. Выберите номер пользователя в диапазоне от 1 до {pageSize} или введите 0 для выхода.\nВвод: ");
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
                Console.WriteLine("Введите логин учётной записи:");
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
                Console.WriteLine("Пользователь не найден");
                Title.Wait();
                return;
            }

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            Console.Clear();
            if (Decrypt.Get((string)user.Attribute("password"), 3) != password && Decrypt.Get((string)admin.Attribute("password"), 3) != password)
            {
                Console.WriteLine("Неверный пароль");
                Title.Wait();
                return;
            }

            Console.WriteLine("Выберите действие:");
            string choice1 = "1. Изменить логин";
            if (login == "ADMIN")
                choice1 += " (ЗАПРЕЩЕНО)";
            Console.WriteLine(choice1);
            Console.WriteLine("2. Изменить пароль");
            int choice;

            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Некорректный выбор");
                Title.Wait();
                return;
            }
            
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введите новый логин:");
                    string newLogin = Console.ReadLine();

                    // проверяем, что новый логин не повторяется
                    if (doc.Descendants("User").Any(x => (string)x.Attribute("login") == newLogin) || login == "ADMIN")
                    {   
                        Console.Clear();
                        if (login != "ADMIN")
                            Console.WriteLine("Пользователь с таким логином уже существует");
                        else
                            Console.WriteLine("Аккаунт администратора должен быть только с таким логином, менять его запрещено!");
                        Title.Wait();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(newLogin))
                    {
                        Console.Clear();
                        Console.WriteLine("Логин не может быть пустым");
                        Title.Wait();
                        return;
                    }

                    user.Attribute("login").Value = newLogin;
                    break;

                case 2:
                    Console.WriteLine("Введите новый пароль:");
                    string newPassword = Console.ReadLine();
                    Console.WriteLine("Повторите новый пароль:");
                    string confirmPassword = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newPassword))
                    {
                        Console.Clear();
                        Console.WriteLine("Пароль не может быть пустым");
                        Title.Wait();
                        return;
                    }

                    if (newPassword != confirmPassword)
                    {
                        Console.Clear();
                        Console.WriteLine("Пароли не совпадают");
                        Title.Wait();
                        return;
                    }

                    user.Attribute("password").Value = Encrypt.Get(newPassword, 3);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Некорректный выбор");
                    Title.Wait();
                    return;
            }

            doc.Save(filePath); // сохраняем измененный XML-файл
            Console.Clear();
            Console.WriteLine("Данные успешно изменены");
            Title.Wait();
        }
    }
}