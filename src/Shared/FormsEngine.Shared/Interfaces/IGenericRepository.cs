namespace FormsEngine.Shared.Interfaces;

// This interface is a marker interface for generic repository implementations. Handy in testing, ORM/DB factory switching.
public interface IGenericRepository<T> where T : class
{
    // Generic Id type V allows for flexibility in the type of identifier used.
    Task<T?> GetByIdAsync<V>(V id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync<V>(V id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
