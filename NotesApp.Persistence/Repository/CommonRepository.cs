using NotesApp.Application.Contracts.Persistence;

namespace NotesApp.Persistence.Repository;

public class CommonRepository : ICommonRepository
{
    public async Task<bool> EmailIdExistsAsync(string emailId)
    {
        await Task.Delay(1000);
        return false;
    }
}
