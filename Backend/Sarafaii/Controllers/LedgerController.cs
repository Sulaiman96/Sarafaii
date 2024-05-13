using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sarafaii.Data;
using Sarafaii.DTOs;
using Sarafaii.Models;

namespace Sarafaii.Controllers;

public class LedgerController(IMapper mapper, ApplicationDbContext dbContext) : BaseApiController(mapper)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LedgerResponse>>> Get()
    {
        var ledgers = await dbContext.Ledgers
            .Include(l => l.Currency)
            .Include(l => l.FromCustomer)
            .Include(l => l.ToCustomer)
            .ToListAsync();
        
        var ledgerDto = mapper.Map<IEnumerable<LedgerResponse>>(ledgers);
        
        return Ok(ledgerDto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LedgerResponse>> GetById(int id)
    {
        var ledgers = await dbContext.Ledgers
            .Include(l => l.Currency)
            .Include(l => l.FromCustomer)
            .Include(l => l.ToCustomer)
            .SingleOrDefaultAsync(l => l.Id == id);
        
        if (ledgers == null)
            return NotFound();
        
        var ledgerDto = mapper.Map<LedgerResponse>(ledgers);
        
        return Ok(ledgerDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LedgerRequest ledgerRequest)
    {
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userName == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var user = await dbContext.Users.FirstAsync(u => u.UserName == userName);
        
        Ledger ledger = new()
        {
            Amount = ledgerRequest.Amount,
            Rate = ledgerRequest.Rate,
            Date = DateTime.UtcNow.Date,
            Currency = await GetCurrency(ledgerRequest.Currency),
            FromCustomer = await GetCustomer(ledgerRequest.FromCustomer),
            ToCustomer = await GetCustomer(ledgerRequest.ToCustomer),
            HasBeenCollected = false,
            User = user
        };
        try
        {
            await dbContext.Ledgers.AddAsync(ledger);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest($"Was not able to save Ledger: {ex.Message}" );
        }

        var ledgerDto = mapper.Map<LedgerResponse>(ledger);
        
        return CreatedAtAction(nameof(GetById), new {id = ledger.Id}, ledgerDto);
    }

    private async Task<Customer> GetCustomer(CustomerDto ledgerRequestFromCustomer)
    {
        var customer = await dbContext.Customers.FirstAsync(c =>
            c.FirstName + " " + c.LastName == ledgerRequestFromCustomer.FullName &&
            c.DateOfBirth == ledgerRequestFromCustomer.DateOfBirth);

        return customer;
    }

    private async Task<Currency> GetCurrency(string ledgerRequestCurrency)
    {
        var currency = await dbContext.Currencies.FirstOrDefaultAsync(c => c.Name == ledgerRequestCurrency);
        if (currency == null)
            throw new Exception("Could not find Currency");

        return currency;
    }
}