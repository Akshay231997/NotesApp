using AutoMapper;
using NotesApp.Application.Features.Identity.Commands.RegisterUser;
using NotesApp.Domain;

namespace NotesApp.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}
