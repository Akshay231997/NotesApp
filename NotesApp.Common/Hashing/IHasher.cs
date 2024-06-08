namespace NotesApp.Common.Hashing;

public interface IHasher
{
    string Hash(string value, byte saltSize = 32);
}
