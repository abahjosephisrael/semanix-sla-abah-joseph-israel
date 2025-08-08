using FormsEngine.Shared.Interfaces;
using FormsService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FormsService.Infrastructure.Persistence.Repositories;
public class GenericRepository<T>(FormsServiceDbContext context) : IGenericRepository<T> where T : class
{
    protected readonly FormsServiceDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync<V>(V id) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<IEnumerable<T>> GetAllAsync<V>(V id) => await _dbSet.ToListAsync();
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;

    public void Delete(T entity) => _dbSet.Remove(entity);
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
}
