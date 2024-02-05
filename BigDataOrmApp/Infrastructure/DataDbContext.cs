using BigDataOrmApp.Domain;
using BigDataOrmApp.Domain.BigData;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrmApp.Infrastructure;

public class DataDbContext : DbContext
{
    public DbSet<TextDocumentEntity> TextDocuments { get; set; } = null!;

    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TextDocument>().ToTable("TextDocument");
    }
}