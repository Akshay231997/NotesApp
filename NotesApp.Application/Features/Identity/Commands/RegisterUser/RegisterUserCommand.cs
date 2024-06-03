using MediatR;

namespace NotesApp.Application.Features.Identity.Commands.RegisterUser;

public record RegisterUserCommand : IRequest<string>
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
}
