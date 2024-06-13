using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sarafaii.Data;
using Sarafaii.DTOs;
using Sarafaii.Models;

namespace Sarafaii.Controllers;

public class CustomerController(IMapper mapper, ApplicationDbContext dbContext) : BaseApiController(mapper)
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
    {
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userName == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var user = await dbContext.Users.FirstAsync(u => u.UserName == userName);

        var customers = await dbContext.Customers.Where(c => c.UserId == user.Id).ToListAsync();

        var customersDto = mapper.Map<IEnumerable<CustomerDto>>(customers);

        return Ok(customersDto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userName == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var user = await dbContext.Users
            .FirstAsync(u => u.UserName == userName);

        var customer = await dbContext.Customers
                .Where(c => c.UserId == user.Id && c.Id == id)
                .SingleOrDefaultAsync();

        if (customer == null)
            return NotFound();

        var customersDto = mapper.Map<CustomerDto>(customer);

        return Ok(customersDto);
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetById(string fullName)
    {
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userName == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var user = await dbContext.Users
            .FirstAsync(u => u.UserName == userName);

        var customers = await dbContext.Customers
            .Where(c => c.UserId == user.Id && (c.FirstName + " " + c.LastName).Equals(fullName, StringComparison.InvariantCultureIgnoreCase))
            .ToListAsync();

        if (customers.Count == 0)
            return NotFound();

        var customersDto = mapper.Map<IEnumerable<CustomerDto>>(customers);

        return Ok(customersDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerDto customerRequest)
    {
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userName == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var user = await dbContext.Users.FirstAsync(u => u.UserName == userName);
        var name = customerRequest.FullName.Split(" ");
        Customer customer = new()
        {
            FirstName = name[0],
            LastName = name[1],
            BirthCertificateUrl = customerRequest.BirthCertificateUrl,
            DateOfBirth = customerRequest.DateOfBirth,
            ProfilePictureUrl = customerRequest.ProfilePictureUrl,
            User = user
        };
        
        try
        {
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest($"Was not able to save Ledger: {ex.Message}" );
        }

        var customerDto = mapper.Map<CustomerDto>(customer);
        
        return CreatedAtAction(nameof(GetById), new {id = customer.Id}, customerDto);
    }
}