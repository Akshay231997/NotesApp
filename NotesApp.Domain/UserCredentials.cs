using NotesApp.Domain.Common;

namespace NotesApp.Domain;

public class UserCredentials : BaseEnity
{
    public string UserId { get; set; }
    public string PasswordHash { get; set; }
}
