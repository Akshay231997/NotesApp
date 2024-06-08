using NotesApp.Domain;

namespace NotesApp.Application.Contracts.Identity;

public interface IToken
{
    /// <summary>
    /// Generates a token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>A string representing the generated token.</returns>
    string Generate(User user);
}
