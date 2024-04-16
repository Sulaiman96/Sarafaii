using Microsoft.EntityFrameworkCore;

namespace Sarafaii.Models;

[PrimaryKey(nameof(CurrencyId), nameof(Date), nameof(UserId))]
public class DailyRate
{
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }
    
    public int UserId { get; set; }
    public AppUser User { get; set; }
}