namespace NotesApp.Common.Audits;

public class Audit : IAudit
{
    public DateTime CreatedOn => DateTime.UtcNow;

    public DateTime UpdatedOn => DateTime.UtcNow;
}
