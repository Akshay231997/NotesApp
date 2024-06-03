namespace NotesApp.Domain.Common;

public class BaseEnity
{
    public string Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}