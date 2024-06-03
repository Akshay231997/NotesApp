namespace NotesApp.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetAsync(string id);
    Task<IEnumerable<T>> GetAsync();
    Task CreateAsync(T entity);
    Task CreateAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}
