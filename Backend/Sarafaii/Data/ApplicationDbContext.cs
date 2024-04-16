using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sarafaii.Models;

namespace Sarafaii.Data;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser, AppRole, int, 
    IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(dbContextOptions)
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    public DbSet<Ledger> Ledgers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
        
        builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
        
        builder.Entity<Ledger>()
            .HasOne(l => l.DailyRate)
            .WithMany()
            .HasForeignKey(l => new { l.DailyRateCurrencyId, l.DailyRateDate, l.DailyRateUserId });

        builder.Entity<Ledger>()
            .HasOne(l => l.User)
            .WithOne(u => u.Ledger)
            .HasForeignKey<Ledger>(l => l.UserId);
        
        builder.Entity<Ledger>()
            .HasOne(l => l.FromCustomer)
            .WithMany()
            .HasForeignKey(l => l.FromCustomerId);

        builder.Entity<Ledger>()
            .HasOne(l => l.ToCustomer)
            .WithMany()
            .HasForeignKey(l => l.ToCustomerId);
        
        builder.Entity<Ledger>()
            .HasOne(l => l.Currency)
            .WithMany() 
            .HasForeignKey(l => l.CurrencyId);

        builder.Entity<ExchangeRate>()
            .HasOne(er => er.Currency)
            .WithMany()
            .HasForeignKey(er => er.CurrencyId);
        
        builder.Entity<DailyRate>()
            .HasOne(dr => dr.Currency)
            .WithMany()
            .HasForeignKey(dr => dr.CurrencyId);
        
        builder.Entity<DailyRate>()
            .HasOne(dr => dr.User)
            .WithMany(u => u.DailyRates)
            .HasForeignKey(dr => dr.UserId);
        
        builder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithMany(u => u.Customers)
            .HasForeignKey(c => c.UserId);
    }
}