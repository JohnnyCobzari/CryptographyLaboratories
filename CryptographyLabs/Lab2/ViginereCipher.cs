using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1.Lab2;

public class VigenereCipher
{
    private readonly Dictionary<char, int> alphabet;

    public VigenereCipher()
    {
        alphabet = new Dictionary<char, int>();
        string letters = "AĂÂBCDEFGHIÎJKLMNOPQRSȘTȚUVWXYZ";

        for (int i = 0; i < letters.Length; i++)
        {
            alphabet[letters[i]] = i;
        }
    }

    private string PrepareText(string text)
    {
        text = text.ToUpper(new System.Globalization.CultureInfo("ro-RO")).Replace(" ", "");

        if (!Regex.IsMatch(text, @"^[A-ZĂÂÎȘȚ]*$"))
        {
            throw new ArgumentException("Invalid input. Only Romanian letters are allowed.");
        }

        return text;
    }

    public string Encrypt(string key, string plaintext)
    {
        plaintext = PrepareText(plaintext);
        key = PrepareText(key);

        StringBuilder result = new StringBuilder();

        for (int i = 0; i < plaintext.Length; i++)
        {
            char currentLetter = key[i % key.Length];

            int indexToAdd = alphabet[currentLetter];
            int originalIndex = alphabet[plaintext[i]];

            int newIndex = (originalIndex + indexToAdd) % alphabet.Count;
            char newLetter = alphabet.First(x => x.Value == newIndex).Key;

            result.Append(newLetter);
        }

        return result.ToString();
    }

    public string Decrypt(string key, string ciphertext)
    {
        ciphertext = PrepareText(ciphertext);
        key = PrepareText(key);

        StringBuilder result = new StringBuilder();

        for (int i = 0; i < ciphertext.Length; i++)
        {
            char currentLetter = key[i % key.Length];

            int indexToSubtract = alphabet[currentLetter];
            int encryptedIndex = alphabet[ciphertext[i]];

            int newIndex = (encryptedIndex - indexToSubtract + alphabet.Count) % alphabet.Count;
            char newLetter = alphabet.First(x => x.Value == newIndex).Key;

            result.Append(newLetter);
        }

        return result.ToString();
    }
}