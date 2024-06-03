namespace NotesApp.Common.Audits;

/// <summary>
/// Interface for audit information, providing properties to track creation and update timestamps.
/// </summary>
public interface IAudit
{
    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedOn { get;}

    /// <summary>
    /// Gets the date and time when the entity was last updated.
    /// </summary>
    public DateTime UpdatedOn { get;}
}
