

using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Contexts;
public class AtmAppContext : DbContext
{
    #region Constructor
    public AtmAppContext(DbContextOptions options) : base(options)
    {

    }
    #endregion

    #region DbSet Properties
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    #endregion

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasData(
            new Account()
            {
                Id=101,
                Name="Raj",
                PIN=1000,
                Balance=2000.00
            }
        );

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany()
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    #endregion

}


