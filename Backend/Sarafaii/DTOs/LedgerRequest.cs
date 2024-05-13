namespace Sarafaii.DTOs;

public class LedgerRequest
{
    public decimal Amount { get; set; }
    public decimal Rate { get; set; }
    public string Currency { get; set; }
    public CustomerDto FromCustomer { get; set; }
    public CustomerDto ToCustomer { get; set; }
}