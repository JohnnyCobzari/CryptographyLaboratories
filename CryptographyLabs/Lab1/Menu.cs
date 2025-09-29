namespace ConsoleApp1.Lab1
{
    public class Menu
    {
        public static void Start()
        {
            Console.WriteLine("=== Caesar Cipher ===");
            Console.WriteLine("Choose cipher mode:");
            Console.WriteLine("1. Normal Caesar (A-Z)");
            Console.WriteLine("2. Custom Caesar (requires key string)");
            Console.Write("Your choice: ");
            string modeChoice = Console.ReadLine();
            CeaserCypher cypher;
            if (modeChoice == "1")
            {
                cypher = new CeaserCypher();
            }
            else if (modeChoice == "2")
            {
                Console.Write("Enter custom key string (min 7 letters): ");
                string customKey = Console.ReadLine() ?? "";

                try
                {
                    cypher = new CeaserCypher(customKey);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.WriteLine("\n=== Operation ===");
            Console.WriteLine("1. Encrypt");
            Console.WriteLine("2. Decrypt");
            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            Console.Write("Enter key (1-25): ");
            int key = int.Parse(Console.ReadLine() ?? "0");
            if (key < 1 || key > 25)
            {
                Console.WriteLine("Key must be between 1 and 25.");
                return;
            }

            Console.Write("Enter text: ");
            string text = Console.ReadLine() ?? "";

            try
            {
                if (choice == "1")
                {
                    string encrypted = cypher.Encrypt(key, text);
                    Console.WriteLine($"Encrypted: {encrypted}");
                }
                else if (choice == "2")
                {
                    string decrypted = cypher.Decrypt(key, text);
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