
using System.ComponentModel.DataAnnotations.Schema;

namespace Sarafaii.Models;

public class Ledger
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
    public bool HasBeenCollected { get; set; }
    public decimal Rate { get; set; }

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    
    public int FromCustomerId { get; set; }
    public Customer FromCustomer { get; set; }
    
    public int ToCustomerId { get; set; }
    public Customer ToCustomer { get; set; }

    public int UserId { get; set; }
    public AppUser User { get; set; }
}