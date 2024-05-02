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
    public DbSet<Customer> Customers { get; set; }
    public DbSet<DailyRate> DailyRates { get; set; }
    public DbSet<Ledger> Ledgers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
        
        builder.Entity<AppUser>()
            .HasMany(u => u.Ledgers)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.Entity<Ledger>()
            .HasOne(l => l.User)
            .WithMany(u => u.Ledgers)
            .HasForeignKey(l => l.UserId);
        
        builder.Entity<Ledger>()
            .HasOne(l => l.FromCustomer)
            .WithMany()
            .HasForeignKey(l => l.FromCustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ledger>()
            .HasOne(l => l.ToCustomer)
            .WithMany()
            .HasForeignKey(l => l.ToCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Ledger>()
            .HasOne(l => l.Currency)
            .WithMany() 
            .HasForeignKey(l => l.CurrencyId);
        
        builder.Entity<Ledger>()
            .Property(l => l.Date)
            .HasColumnType("date");

        builder.Entity<ExchangeRate>()
            .HasOne(er => er.Currency)
            .WithMany()
            .HasForeignKey(er => er.CurrencyId);
        
        builder.Entity<ExchangeRate>()
            .Property(er => er.LastUpdated)
            .HasColumnType("date");
        
        builder.Entity<DailyRate>()
            .HasOne(dr => dr.Currency)
            .WithMany()
            .HasForeignKey(dr => dr.CurrencyId);
        
        builder.Entity<DailyRate>()
            .HasOne(dr => dr.User)
            .WithMany(u => u.DailyRates)
            .HasForeignKey(dr => dr.UserId);
        
        builder.Entity<DailyRate>()
            .Property(dr => dr.Date)
            .HasColumnType("date");
        
        builder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithMany(u => u.Customers)
            .HasForeignKey(c => c.UserId);
        
        builder.Entity<Customer>()
            .Property(c => c.DateOfBirth)
            .HasColumnType("date");
    }
    
    public void ResetAutoIncrementValues()
    {
        var tables = new[]
        {
            "Ledgers",
            "ExchangeRates",
            "Customers",
            "Currencies",
            "AspNetUsers",
            "AspNetUserClaims",
            "AspNetRoles",
            "AspNetRoleClaims"
        }; 
        
        foreach (var table in tables)
        {
            Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('{table}', RESEED, 1);");
        }
    }
}