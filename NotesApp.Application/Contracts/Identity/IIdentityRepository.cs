using NotesApp.Domain;

namespace NotesApp.Application.Contracts.Identity;

/// <summary>
/// Interface for handling identity related operations.
/// </summary>
public interface IIdentityRepository
{
    /// <summary>
    /// Asynchronously registers a new user in the system.
    /// </summary>
    /// <param name="user">The user object containing user details to be registered.</param>
    /// <returns>A task whose result is auth token.</returns>
    Task<string> RegisterUserAsync(User user);
}
