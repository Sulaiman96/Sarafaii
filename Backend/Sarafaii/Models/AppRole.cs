using Microsoft.AspNetCore.Identity;

namespace Sarafaii.Models;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}