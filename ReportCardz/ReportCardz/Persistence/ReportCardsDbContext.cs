using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using ReportCardz.Models;

namespace ReportCardz.Persistence;

public class ReportCardsDbContext: DbContext
{
    public DbSet<Child> Children { get; set; }
    
    public static ReportCardsDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<ReportCardsDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
    
    public ReportCardsDbContext(DbContextOptions<ReportCardsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Child>()
            .ToCollection("children")
            .Property(x => x.LastModified)
            .IsConcurrencyToken();
    }
}