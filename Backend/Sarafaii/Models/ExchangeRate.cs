
namespace Sarafaii.Models;

public class ExchangeRate
{
    public int Id { get; set; }
    public decimal Rate { get; set; }
    public DateTime LastUpdated { get; set; }
    
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
}