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
                        Title.Set($"{Lang.GetText("title_error_input_keys")}");
                        Console.WriteLine($"{Lang.GetText("Access_key_error_0")}\n{Lang.GetText("string_restart")}");
                        Config.Set("AutoKey", false);
                        Title.Wait();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write(
                        $"{Lang.GetText("Access_key_need_0")}.\n" +
                        $"{Lang.GetText("Access_key_need_1")}\n" +
                        $"{Lang.GetText("string_input")}: "
                        );
                    key = Console.ReadLine();
                    if (CheckAccess(key))
                    {
                        if (!Config.Get("ConfirmSaveKey", false))
                        {
                            Console.Clear();
                            Console.WriteLine($"{Lang.GetText("Access_menu_save_key_0")}\n{Lang.GetText("Access_menu_save_key_1")}");
                            Console.Write(
                                $"1. {Lang.GetText("Access_save_choice_0")}\n" +
                                $"2. {Lang.GetText("Access_save_choice_1")}\n" +
                                $"{Lang.GetText("string_input")}: "
                                );
                            switch (Choice.Get())
                            {
                                default:
                                    Config.Set("AccessKey", Encrypt.Get(key, 2));
                                    Config.Set("AutoKey", true);
                                    Console.Clear();
                                    Console.WriteLine($"{Lang.GetText("string_key_done")}");
                                    Title.Wait();
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine($"{Lang.GetText("Access_key_not_saved_0")}\n{Lang.GetText("Access_key_not_saved_1")}");
                                    Title.Wait();
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