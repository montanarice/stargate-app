using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore.Internal;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Exceptions;

namespace StargateAPI.Business.Data;

// TODO: Extract this class to an IDataAccess interface for handy database swapping when testing
public class StargateContext : DbContext, IDataAccess
{
    public IDbConnection Connection => Database.GetDbConnection();
    public DbSet<Person> People { get; set; }
    public DbSet<AstronautDetail> AstronautDetails { get; set; }
    public DbSet<AstronautDuty> AstronautDuties { get; set; }
    public DbSet<LogTableEntry> LogTables { get; set; }

    public StargateContext(DbContextOptions<StargateContext> options)
        : base(options)
    {
    }

    public new async ValueTask<T> AddAsync<T>(T entity, CancellationToken token)
    {
        // TODO: Do reflection property access better
        var addType = typeof(T);

        // TODO REVIEW: Code smell tech debt time! Fix later
        if (addType.IsAssignableFrom(typeof(Person)))
        {
            await People.AddAsync((entity as Person)!, token);
        }

        else if (addType.IsAssignableFrom(typeof(AstronautDetail)))
        {
            await AstronautDetails.AddAsync((entity as AstronautDetail)!, token);
        }

        else if (addType.IsAssignableFrom(typeof(AstronautDuty)))
        {
            await AstronautDuties.AddAsync((entity as AstronautDuty)!, token);
        }

        else
        {
            throw new BusinessRuleBrokenException("Attempted to add and entity matching no known database generic");
        }

        return entity;
    }

    public async Task<int> UpdateAsync(CancellationToken token)
    {
        return await SaveChangesAsync(token);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string query, CancellationToken token)
    {
        // TODO BREAKING: Get DbSet property by generic reflection, return query on that DbSet
        return await Connection.QueryAsync<T>(query);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StargateContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}