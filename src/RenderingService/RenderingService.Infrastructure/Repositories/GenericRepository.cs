using FormsEngine.Shared.Interfaces;
using System.Data;
using Dapper;
using RenderingService.Domain.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.Json;

namespace RenderingService.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    private readonly IDbConnection _connection;
    private readonly string _tableName;
    public GenericRepository(IDbConnection connection)
    {

        _connection = connection;
        _tableName = "public.\"" + typeof(T).Name.ToString() +  "s\"";
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
    
    public async Task<IEnumerable<T>> GetAllAsync<V>(V id)
    {
        var raw = await _connection.QueryAsync($"""SELECT * FROM {_tableName} WHERE tenant_id = @Id AND state = 'Published'""", new {id});

        var renderedForms = raw.Select(r => new Form(
            id: (Guid)r.id,
            name: (string)r.name,
            description: (string)r.description,
            version: (int)r.version,
            state: (string)r.state,
            sections: JsonSerializer.Deserialize<List<Section>>((string)r.sections)!,
            created_at: (DateTime)r.created_at,
            created_by: (string?)r.created_by,
            updated_at: (DateTime?)r.updated_at,
            updated_by: (string?)r.updated_by
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
