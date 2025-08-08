using FormsEngine.Shared.Interfaces;
using System.Data;
using Dapper;
using RenderingService.Domain.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RenderingService.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    private readonly IDbConnection _connection;
    private readonly string _tableName;
    public GenericRepository(IDbConnection connection)
    {

        _connection = connection;
        _tableName = typeof(T).Name.ToLower()+"s"; // PostgreSQL tables are typically lowercase
    }
    public Task AddAsync(T entity)
    {
        throw new InvalidOperationException();
    }

    public void Delete(T entity)
    {
        throw new InvalidOperationException();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM {_tableName}";
        return await _connection.QueryAsync<T>(sql);
    }

    //public async Task<IEnumerable<T>> GetAllAsync<V>(V id)
    //{
    //    const string sql = """
    //    SELECT *
    //    FROM   "Forms"
    //    WHERE  "tenant_id" = @Id AND "state" = 'Published'
    //    """;

    //    return await _connection.QueryAsync<T>(sql, new { id});
    //}
    
    public async Task<IEnumerable<T>> GetAllAsync<V>(V id)
    {
        var raw = await _connection.QueryAsync("""SELECT * FROM public."Forms" WHERE tenant_id = @Id AND state = 'Published'""",new {id});

        var renderedForms = raw.Select(row => new RenderedForm(
            row.id,
            row.name,
            row.description,
            row.version,
            row.state,
            row.sections,
            row.created_at,
            row.created_by,
            row.updated_at,
            row.updated_by
        ));
        return renderedForms.Cast<T>().ToList();
    }

    public Task<T?> GetByIdAsync<V>(V id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException();
    }

    public void Update(T entity)
    {
        throw new InvalidOperationException();
    }
}
