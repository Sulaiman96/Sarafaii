using System.ComponentModel.DataAnnotations;

namespace Sarafaii.Models;

public class Ledger
{
    [Key]
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    
    public int FromAppUserId { get; set; }
    public AppUser FromAppUser { get; set; }
    
    public int ToAppUserId { get; set; }
    public AppUser ToAppUser { get; set; }

    public int ExchangeRateId { get; set; }
    public ExchangeRate ExchangeRate { get; set; }
}