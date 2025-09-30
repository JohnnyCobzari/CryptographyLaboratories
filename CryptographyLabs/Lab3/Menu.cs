using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1.Lab2
{
    public class VigenereMenu
    {
        public static void Start()
        {
            VigenereCipher cipher = new VigenereCipher();

            while (true)
            {
                Console.WriteLine("\n=== Vigenere Cipher ===");
                Console.WriteLine("1. Encrypt");
                Console.WriteLine("2. Decrypt");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                if (choice == "3")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                Console.InputEncoding = Encoding.UTF8;
                Console.OutputEncoding = Encoding.UTF8;
                Console.Write("Enter key (Romanian letters only, min 7 characters): ");
                string key = Console.ReadLine() ?? "";

                if (key.Length < 7)
                {
                    Console.WriteLine("Error: Key must be at least 7 characters long.");
                    continue;
                }

                if (!Regex.IsMatch(key.ToUpper(new System.Globalization.CultureInfo("ro-RO")), @"^[A-ZĂÂÎȘȚ]+$"))
                {
                    Console.WriteLine("Error: Key must contain only Romanian letters.");
                    continue;
                }

                Console.Write("Enter text: ");
                string text = Console.ReadLine() ?? "";

                try
                {
                    if (choice == "1")
                    {
                        string encrypted = cipher.Encrypt(key, text);
                        Console.WriteLine($"Encrypted: {encrypted}");
                    }
                    else if (choice == "2")
                    {
                        string decrypted = cipher.Decrypt(key, text);
                        Console.WriteLine($"Decrypted: {decrypted}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid option.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}