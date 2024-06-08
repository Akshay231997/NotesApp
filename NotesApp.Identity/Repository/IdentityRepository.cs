using MongoDB.Bson;
using NotesApp.Application.Contracts.Identity;
using NotesApp.Common.Guards;
using NotesApp.Domain;
using NotesApp.Identity.DBContext;
using NotesApp.Identity.DBContext.ApplicationDBContext;
using NotesApp.Identity.DBContext.AuthDBContext;

namespace NotesApp.Identity.Repository;

public class IdentityRepository : IIdentityRepository
{
    private readonly IAuthDBContext _authDBContext;
    private readonly IApplicationDBContext _applicationDBContext;

    public IdentityRepository(IAuthDBContext authDBContext, IApplicationDBContext applicationDBContext)
    {
        _authDBContext = authDBContext;
        _applicationDBContext = applicationDBContext;
    }

    public async Task CreateUserAsync(User user)
    {
        Guard.GuardAgainstNull(nameof(user), user);

        await _applicationDBContext.GetMongoCollection<User>().InsertOneAsync(user);
    }

    public async Task InsertUserCredsAsync(UserCredentials userCredentials)
    {
        Guard.GuardAgainstNull(nameof(userCredentials), userCredentials);

        await _authDBContext.GetMongoCollection<UserCredentials>().InsertOneAsync(userCredentials);
    }
}
