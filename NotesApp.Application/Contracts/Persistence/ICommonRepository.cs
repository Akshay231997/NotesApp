namespace NotesApp.Application.Contracts.Persistence;

/// <summary>
/// Interface for common repository operations.
/// </summary>
public interface ICommonRepository
{
    /// <summary>
    /// Asynchronously checks if the specified email ID exists in the system.
    /// </summary>
    /// <param name="emailId">The email ID to check for existence.</param>
    /// <returns>The task result contains a boolean value indicating whether the email ID exists.</returns>
    Task<bool> EmailIdExistsAsync(string emailId);
}
