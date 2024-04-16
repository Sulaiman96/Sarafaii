using Microsoft.EntityFrameworkCore;

namespace Sarafaii.Models;

[PrimaryKey(nameof(Id), nameof(Date))]
public class ExchangeRate
{
    public int Id { get; set; }
    public decimal Rate { get; set; }
    public DateTime Date { get; set; }
    
    public int CurrencyID { get; set; }
    public Currency Currency { get; set; }
}