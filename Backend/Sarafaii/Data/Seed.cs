using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sarafaii.Models;

namespace Sarafaii.Data;

public class Seed
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync())
            return;
        
        var userData = await File.ReadAllTextAsync("Data/Seed Data/UserSeedData.json");
        
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, Options);

        var roles = new List<AppRole>
        {
            new AppRole {Name = "Member"},
            new AppRole {Name = "Admin"},
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
        
        var admin = new AppUser
        {
            UserName = "admin@gmail.com",
            FirstName = "admin",
            LastName = "admin",
            Email = "admin@gmail.com"
        };

        await userManager.CreateAsync(admin, "Password1");
        await userManager.AddToRoleAsync(admin, "Admin");
    
        foreach (var user in users)
        {
            await userManager.CreateAsync(user, "Password1");

            await userManager.AddToRoleAsync(user, "Member");
        }
    }
    
    public static async Task SeedLedger(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if (await context.Ledgers.AnyAsync())
            return;

        var ledgerData = await File.ReadAllTextAsync("Data/Seed Data/LedgerSeedData.json");

        var ledgers = JsonSerializer.Deserialize<List<Ledger>>(ledgerData, Options);

        // foreach (var ledger in ledgers)
        // {
        //     var existingUser = await userManager.FindByIdAsync(ledger.UserId.ToString());
        //     if (existingUser != null)
        //     {
        //         ledger.User = existingUser;
        //     }
        //     context.Ledgers.Add(ledger);
        //    
        // }

        await using (var transaction = await context.Database.BeginTransactionAsync())
        {
            try
            {
                foreach (var ledger in ledgers)
                {
                    var existingUser = await userManager.FindByIdAsync(ledger.UserId.ToString());
                    if (existingUser == null) 
                        continue;
                    ledger.User = existingUser;
                    context.Ledgers.Add(ledger);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync(); // Explicitly commit the transaction
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Roll back in case of an error
                Console.WriteLine("Error during transaction: " + ex.Message);
            }
        }
        
        await context.SaveChangesAsync();
    }
    
    public static async Task SeedCurrency(ApplicationDbContext context)
    {
        if (await context.Currencies.AnyAsync())
            return;

        var currencyData = await File.ReadAllTextAsync("Data/Seed Data/CurrencySeedData.json");

        var currencies = JsonSerializer.Deserialize<List<Currency>>(currencyData, Options);

        foreach (var currency in currencies)
        {
            context.Currencies.Add(currency);
        }

        await context.SaveChangesAsync();
    }
    
    public static async Task SeedCustomer(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if (await context.Customers.AnyAsync())
            return;

        var customerData = await File.ReadAllTextAsync("Data/Seed Data/CustomerSeedData.json");

        var customers = JsonSerializer.Deserialize<List<Customer>>(customerData, Options);

        foreach (var customer in customers)
        {
            var existingUser = userManager.Users.FirstOrDefault(user => user.Id == customer.UserId);
            if (existingUser != null)
            {
                customer.User = existingUser;
            }
            context.Customers.Add(customer);
        }

        await context.SaveChangesAsync();
    }
    
    public static async Task SeedExchangeRate(ApplicationDbContext context)
    {
        if (await context.ExchangeRates.AnyAsync())
            return;

        var exchangeRateData = await File.ReadAllTextAsync("Data/Seed Data/ExchangeRateSeedData.json");

        var exchangeRates = JsonSerializer.Deserialize<List<ExchangeRate>>(exchangeRateData, Options);

        foreach (var exchangeRate in exchangeRates)
        {
            context.ExchangeRates.Add(exchangeRate);
        }

        await context.SaveChangesAsync();
    }
    
    public static async Task SeedDailyRate(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if (await context.DailyRates.AnyAsync())
            return;

        var dailyRateData = await File.ReadAllTextAsync("Data/Seed Data/DailyRateSeedData.json");

        var dailyRates = JsonSerializer.Deserialize<List<DailyRate>>(dailyRateData, Options);

        foreach (var dailyRate in dailyRates)
        {
            var existingUser = userManager.Users.FirstOrDefault(user => user.Id == dailyRate.UserId);
            if (existingUser != null)
            {
                dailyRate.User = existingUser;
            }
            context.DailyRates.Add(dailyRate);
        }

        await context.SaveChangesAsync();
    }
    
    public static void ResetAutoIncrement(ApplicationDbContext context)
    {
        context.ResetAutoIncrementValues();
    }
}