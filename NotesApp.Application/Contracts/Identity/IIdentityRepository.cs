using NotesApp.Domain;

namespace NotesApp.Application.Contracts.Identity;

/// <summary>
/// Interface for handling identity related operations.
/// </summary>
public interface IIdentityRepository
{
    /// <summary>
    /// Asynchronously creates a new user in the system.
    /// </summary>
    /// <param name="user">The user object containing user details to be registered.</param>
    Task CreateUserAsync(User user);

    Task InsertUserCredsAsync(UserCredentials userCredentials);
}
