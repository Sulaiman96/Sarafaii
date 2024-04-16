using Microsoft.AspNetCore.Identity;

namespace Sarafaii.Models;

public class AppUser : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
    public ICollection<DailyRate> DailyRates { get; set; }
    public ICollection<Customer> Customers { get; set; }
    public Ledger Ledger { get; set; }
}