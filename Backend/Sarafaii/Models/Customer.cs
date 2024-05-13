
using System.ComponentModel.DataAnnotations.Schema;

namespace Sarafaii.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }
    public string? BirthCertificateUrl { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    public int UserId { get; set; }
    public AppUser User { get; set; }
}