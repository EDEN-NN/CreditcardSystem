namespace CreditcardSystem.Infra.Data;

using CreditcardSystem.Domain.Models;
using CreditcardSystem.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

public class CredicardDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Creditcard> Creditcards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(
            optionsBuilder.UseMySQL(
                "Server=localhost,3306;Database=creditcard_system;User ID=root;Password=root"
            )
        );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new CreditcardMap());
    }
}
