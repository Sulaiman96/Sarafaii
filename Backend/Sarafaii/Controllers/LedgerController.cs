using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sarafaii.Data;
using Sarafaii.DTOs;

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
}