using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class General
    {
        public static string MenuStr0 =
            "Главное меню | VeterinaryClinicRB\n" +
            "Сегодня было совершено";
        public static string MenuStr1 =
            $"Выберите пункт, с которым желаете работать:\n" +
            "1. Врачи\n" +
            "2. Приёмы\n" +
            "3. Пациенты\n" +
            "4. Статистика\n" +
            "5. Аккаунт (В процессе...)\n" +
            "6. Настройка цвета (NEW)\n" +
            "0. Выход из программы";
        public static void Menu()
        {
            try
            {
                bool enableMenu = true;
                while (enableMenu)
                {
                    Statistic statistic = new Statistic("./database/statistic.xml");
                    int todayCount = statistic.GetTodayCount();
                    
                    Title.Set("Главная");
                    Console.Clear();
                    Console.Write($"{MenuStr0} {todayCount} приёмов\n{MenuStr1}\nВвод: ");

                    switch (Choice.Get())
                    {
                        case 1:
                            Doctor.Menu(); // Меню врача (Doctor.cs)
                            break;
                        case 2:
                            Admission.Menu(); // Меню приёмов (Admission.cs)
                            break;
                        case 3:
                            Pacientes.Menu(); // Меню пациентов (Pacientes.cs)
                            break;
                        case 4:
                            Statistic.Menu(); // Меню статистики (./Statistic/Main.cs)
                            break;
                        case 5:
                            Account.Menu(); // Меню аккаунта (Account.cs)
                            break;
                        case 6:
                            Color.Menu(); // Меню аккаунта (Color.cs)
                            break;
                        case 0:
                            enableMenu = false;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Правильно введите номер пункта!");
                            Title.Set("Ошибка");
                            Title.Wait();
                            break;
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                // Вывод сообщения об ошибке
                Console.Clear();
                Title.Set("Ошибка");
                Console.WriteLine(
                    "Возникла ошибка. Программа вернёт вас на Главную\n" +
                    "Чтобы не столкнуться с данной проблемой вновь, пишите в правильном формате!\n" +
                    $"[DEBUG]: {ex.Message}\n\n" +
                    "Если ничего не помогает, напишите нам об ошибке\n" +
                    "[link]https://discord.gg/JkkEKuT7P3[/link] (CTRL +  ПКМ по ссылке)"
                    );
                Title.Wait();
            }
        }
    }
}