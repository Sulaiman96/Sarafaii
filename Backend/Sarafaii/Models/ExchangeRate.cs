
using System.ComponentModel.DataAnnotations.Schema;

namespace Sarafaii.Models;

public class ExchangeRate
{
    public int Id { get; set; }
    public decimal Rate { get; set; }
    [Column(TypeName = "date")]
    public DateTime LastUpdated { get; set; }
    
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
}