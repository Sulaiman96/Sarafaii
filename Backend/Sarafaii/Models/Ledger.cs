
namespace Sarafaii.Models;

public class Ledger
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool HasBeenCollected { get; set; }

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    
    public int FromCustomerId { get; set; }
    public Customer FromCustomer { get; set; }
    
    public int ToCustomerId { get; set; }
    public Customer ToCustomer { get; set; }
    
    //Daily Rate composite key relationship
    public int DailyRateCurrencyId { get; set; }
    public DateTime DailyRateDate { get; set; }
    public int DailyRateUserId { get; set; }
    public DailyRate DailyRate { get; set; }

    public int UserId { get; set; }
    public AppUser User { get; set; }
}