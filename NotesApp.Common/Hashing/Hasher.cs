using NotesApp.Common.Guards;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace NotesApp.Common.Hashing;

public abstract class Hasher : IHasher
{
    public string Hash(string value, byte saltSize = 32)
    {
        Guard.GuardAgainstNull(nameof(value), value);

        byte[] salt = GenerateSalt(saltSize);

        return ComputeHash(value, salt);
    }

    protected abstract byte[] GenerateSalt(int size);

    protected abstract string ComputeHash(string input, byte[] saltBytes);
}

public class SHA256Hasher : Hasher
{
    protected override byte[] GenerateSalt(int size)
    {
        byte[] salt = new byte[size];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        return salt;
    }

    protected override string ComputeHash(string input, byte[] saltBytes)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] inputWithSaltBytes = new byte[inputBytes.Length + saltBytes.Length];

            // Combine input bytes and salt bytes
            Buffer.BlockCopy(inputBytes, 0, inputWithSaltBytes, 0, inputBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, inputWithSaltBytes, inputBytes.Length, saltBytes.Length);

            // Compute the hash
            byte[] hash = sha256.ComputeHash(inputWithSaltBytes);

            return Convert.ToBase64String(hash);
        }
    }
}