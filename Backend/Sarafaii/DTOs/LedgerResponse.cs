namespace Sarafaii.DTOs;

public class LedgerResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public decimal Rate { get; set; }
    public DateTime Date { get; set; }
    public string Currency { get; set; }
    public CustomerDto FromCustomer { get; set; }
    public CustomerDto ToCustomer { get; set; }
    public bool HasBeenCollected { get; set; }
}