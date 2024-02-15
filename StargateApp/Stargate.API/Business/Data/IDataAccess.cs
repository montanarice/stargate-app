using Microsoft.EntityFrameworkCore;
using System.Data;

namespace StargateAPI.Business.Data;

// TODO: Currently making compromises in this interface leaving EF Core types in the interface. Return to this tech. debt once minimal viable product is working.
public interface IDataAccess
{
    // TODO: remove EFCore properties once fully refactored away from StargateContext
    public IDbConnection Connection { get; }
    public DbSet<Person> People { get; set; }
    public DbSet<AstronautDetail> AstronautDetails { get; set; }
    public DbSet<AstronautDuty> AstronautDuties { get; set; }

    // TODO REFACTOR: Replace all StargateContext usages of DbSet<T>.AddAsync() with this
    public ValueTask<T> AddAsync<T>(T entity, CancellationToken token);

    // TODO REFACTOR: Replace all StargateContext usages of SaveChangesAsync() with this
    public Task<int> UpdateAsync(CancellationToken token);

    // TODO REFACTOR: Replace all StargateContext usages of Connection.QueryAsync() with this
    // TODO IMPORTANT: Refactor string query into enum of CRUD operations. Let the concrete classes sort out how to query in their own way.
    public Task<IEnumerable<T>> QueryAsync<T>(string query, CancellationToken token);
}