namespace Auto_Circuit.Interfaces;

public interface IRepository
{
    IQueryable<T> GetData<T>() where T : class;
    Task<T> GetByIdAsync<T>(string id) where T : class;
    Task AddAsync<T>(T entity) where T : class;
    Task UpdateAsync<T>(T entity) where T : class;
    Task DeleteAsync<T>(string id) where T : class;
}
