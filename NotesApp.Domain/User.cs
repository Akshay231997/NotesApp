using NotesApp.Domain.Common;

namespace NotesApp.Domain;

public class User : BaseEnity
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string EmailId { get; set; }
}
