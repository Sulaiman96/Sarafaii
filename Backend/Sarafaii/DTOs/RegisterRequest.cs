using System.ComponentModel.DataAnnotations;

namespace Sarafaii.DTOs;

public class RegisterRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}