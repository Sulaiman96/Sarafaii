namespace Sarafaii.DTOs;

public class CustomerDto
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? BirthCertificateUrl { get; set; }
    public string? ProfilePictureUrl { get; set; }
}