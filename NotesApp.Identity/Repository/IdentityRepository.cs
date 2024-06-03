using NotesApp.Application.Contracts.Identity;
using NotesApp.Domain;

namespace NotesApp.Identity.Repository;

public class IdentityRepository : IIdentityRepository
{
    public async Task<string> RegisterUserAsync(User user)
    {
        await Task.Delay(1000);
        return "JWTToken";
    }
}
