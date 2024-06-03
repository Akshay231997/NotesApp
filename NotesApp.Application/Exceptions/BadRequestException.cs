namespace NotesApp.Application.Exceptions;

public class BadRequestException : Exception
{
    public IDictionary<string, string[]> ValidationErrors { get; set; }

    public BadRequestException(string message) : base(message)
    {

    }

    public BadRequestException(string message, IDictionary<string, string[]> ValidationErrors) : base(message)
    {
        this.ValidationErrors = ValidationErrors;
    }
}
