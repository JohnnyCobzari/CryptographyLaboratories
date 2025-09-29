using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1.Lab1;

public class CeaserCypher
{
    private readonly Dictionary<int, char> alphabet;

    public CeaserCypher()
    {
        alphabet = new Dictionary<int, char>();
        for (int i = 0; i < 26; i++)
        {
            alphabet[i] = (char)('A' + i);
        }
    }

    public CeaserCypher(string key)
    {
        if (string.IsNullOrWhiteSpace(key) || key.Length < 7)
        {
            throw new ArgumentException("Key must be at least 7 letters long.");
        }

        key = key.ToUpper();
        if (!Regex.IsMatch(key, @"^[A-Z]+$"))
        {
            throw new ArgumentException("Key must contain only letters A-Z.");
        }

        List<char> keyChars = key.Distinct().ToList();
        List<char> fullAlphabet = keyChars
            .Concat(Enumerable.Range('A', 26).Select(x => (char)x).Where(c => !keyChars.Contains(c))).ToList();
        alphabet = new Dictionary<int, char>();
        for (int i = 0; i < fullAlphabet.Count; i++)
        {
            alphabet[i] = fullAlphabet[i];
        }
    }

    private string PrepareText(string text)
    {
        text = text.ToUpper().Replace(" ", "");
        if (!Regex.IsMatch(text, @"^[A-Z]*$"))
        {
            throw new ArgumentException("Invalid input. Only letters A-Z are allowed.");
        }

        return text;
    }

    public string Encrypt(int key, string plaintext)
    {
        plaintext = PrepareText(plaintext);
        StringBuilder result = new StringBuilder();
        foreach (char c in plaintext)
        {
            int originalIndex = alphabet.First(x => x.Value == c).Key;
            int newIndex = (originalIndex + key) % 26;
            result.Append(alphabet[newIndex]);
        }

        return result.ToString();
    }

    public string Decrypt(int key, string ciphertext)
    {
        ciphertext = PrepareText(ciphertext);
        StringBuilder result = new StringBuilder();
        foreach (char c in ciphertext)
        {
            int originalIndex = alphabet.First(x => x.Value == c).Key;
            int newIndex = (originalIndex - key+26) % 26;
            result.Append(alphabet[newIndex]);
        }

        return result.ToString();
    }
}