using System;
using System.Collections.Generic;
using System.Text;

namespace alms.application.installer.library.Helpers
{
    public static class InputHelper
    {
        public static Tuple<string, string, string> ProcessDatabaseCredentialsInput()
        {
            var server = string.Empty;
            var customer = string.Empty;
            var password = string.Empty;
            while (true)
            {
                Console.Write("Enter server host: ");
                server = Console.ReadLine();
                if (!string.IsNullOrEmpty(server))
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Enter customer name: ");
                customer = Console.ReadLine();
                if (!string.IsNullOrEmpty(customer))
                {
                    break;
                }
            }
            Console.Write("Enter password: ");
            password = GetConsolePassword();

            return new Tuple<string, string, string>(server, customer, password);
        }

        private static string GetConsolePassword()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write('*');
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }

        public static int IntroduceHelper(ConsoleKeyInfo readKey)
        {
            switch (readKey.Key)
            {
                case ConsoleKey.D0:
                    return 0;
                case ConsoleKey.D1:
                    return 1;
                case ConsoleKey.D2:
                    return 2;
                case ConsoleKey.D3:
                    return 3;
                case ConsoleKey.D4:
                    return 4;
                case ConsoleKey.D5:
                    return 5;
                case ConsoleKey.D6:
                    return 6;
                case ConsoleKey.D7:
                    return 7;
                case ConsoleKey.D8:
                    return 8;
                case ConsoleKey.D9:
                    return 9;
                case ConsoleKey.NumPad0:
                    return 0;
                case ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.NumPad2:
                    return 2;
                case ConsoleKey.NumPad3:
                    return 3;
                case ConsoleKey.NumPad4:
                    return 4;
                case ConsoleKey.NumPad5:
                    return 5;
                case ConsoleKey.NumPad6:
                    return 6;
                case ConsoleKey.NumPad7:
                    return 7;
                case ConsoleKey.NumPad8:
                    return 8;
                case ConsoleKey.NumPad9:
                    return 9;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
