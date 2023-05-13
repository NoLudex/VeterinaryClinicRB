using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinicRB
{
    public partial class AccessKey
    {
        public static int j = 5;
        public static bool Check(bool autoKey, bool loginLog)
        {
            string key = "";
            while (true)
            {
                if (loginLog)
                    return true;
                else if (autoKey)
                {
                    key = Decrypt.Get(Config.Get("AccessKey"), 2);
                    if (CheckAccess(key))
                    {
                        Config.Set("AccessKey", Encrypt.Get(key, 2));
                        return true;
                    }
                    else
                    {
                        Console.Clear();
                        Title.Set("Ошибка авто-ключа");
                        Console.WriteLine("Ключ установленный в файле настроек является недействительным\nПерезапустите приложение, чтобы продолжить");
                        Config.Set("AutoKey", false);
                        Title.Wait();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write(
                        "Чтобы иметь доступ к программе нужен специальный ключ.\n" +
                        "Вы можете обратиться к администрации для получения данного ключа\n" +
                        "Ввод: "
                        );
                    key = Console.ReadLine();
                    if (CheckAccess(key))
                    {
                        if (!Config.Get("ConfirmSaveKey", false))
                        {
                            Console.Clear();
                            Console.WriteLine("Желаете ли вы сохранить ключ доступа\nЧтобы не вводить его снова?");
                            Console.Write(
                                "1. Хочу сохранить ключ (Рекомендуется)\n" +
                                "2. Вводить каждый раз при входе\n" +
                                "Ввод: "
                                );
                            switch (Choice.Get())
                            {
                                default:
                                    Config.Set("AccessKey", Encrypt.Get(key, 2));
                                    Config.Set("AutoKey", true);
                                    Console.Clear();
                                    Console.WriteLine("Ключ успешно сохранен!");
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("Ключ не будет сохранен и вам нужно вводить его каждый раз при входе\nЕсли вы желаете изменить это, то сможете сделать это в настройках аккаунта!");
                                    break;
                            }
                            Title.Wait();
                            Config.Set("ConfirmSaveKey", true);
                        }
                        Config.Set("AccessKey", Encrypt.Get(key, 2));
                        return true;
                    }
                    else
                    {
                        AccessKey.Error(j);
                        j += 5;
                    }
                    
                }
            }

            // if (CheckAccess(key))
            //     return true;
            // else
            // {
            //     Console.Clear();
            //     Title.Set("Верификация");
            //     Console.Write(
            //         "Чтобы иметь доступ к программе нужен специальный ключ.\n" +
            //         "Вы можете обратиться к администрации для получения данного ключа\n" +
            //         "Ввод: "
            //         );
            //     return false;
            // }
        }
    }
}