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

        SeedData(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        //add seed data
        modelBuilder.Entity<Person>()
            .HasData(
                new Person
                {
                    Id = 1,
                    Name = "John Doe"
                },
                new Person
                {
                    Id = 2,
                    Name = "Jane Doe"
                }
            );

        modelBuilder.Entity<AstronautDetail>()
            .HasData(
                new AstronautDetail
                {
                    Id = 1,
                    PersonId = 1,
                    CurrentRank = Rank.Colonel,
                    CurrentDutyTitle = DutyTitle.RETIRED,
                    CareerStartDate = new DateTime(1990, 1, 1),
                    CareerEndDate = new DateTime(2010, 1, 2)
                },
                new AstronautDetail
                {
                    Id = 2,
                    PersonId = 2,
                    CurrentRank = Rank.General,
                    CurrentDutyTitle = DutyTitle.Administration,
                    CareerStartDate = new DateTime(1995, 2, 2)
                }
            );

        modelBuilder.Entity<AstronautDuty>()
            .HasData(
                new AstronautDuty
                {
                    Id = 1,
                    PersonId = 1,
                    DutyStartDate = new DateTime(1990, 1, 1),
                    DutyEndDate = new DateTime(2000, 12, 30),
                    DutyTitle = DutyTitle.OrderCreator,
                    Rank = Rank.Captain
                },
                new AstronautDuty
                {
                    Id = 2,
                    PersonId = 1,
                    DutyStartDate = new DateTime(2001, 1, 1),
                    DutyEndDate = new DateTime(2010, 1, 2),
                    DutyTitle = DutyTitle.MoraleBooster,
                    Rank = Rank.Captain
                },
                new AstronautDuty
                {
                    Id = 3,
                    PersonId = 1,
                    DutyStartDate = new DateTime(2010, 1, 3),
                    DutyTitle = DutyTitle.RETIRED,
                    Rank = Rank.Colonel
                },
                new AstronautDuty
                {
                    Id = 4,
                    PersonId = 2,
                    DutyStartDate = new DateTime(1995, 2, 2),
                    DutyEndDate = new DateTime(2000, 5, 5),
                    DutyTitle = DutyTitle.OrderCreator,
                    Rank = Rank.Captain
                },
                new AstronautDuty
                {
                    Id = 5,
                    PersonId = 2,
                    DutyStartDate = new DateTime(2000, 5, 6),
                    DutyTitle = DutyTitle.Administration,
                    Rank = Rank.General
                }
            );
    }
}