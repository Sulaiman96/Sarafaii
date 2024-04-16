using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sarafaii.DTOs;
using Sarafaii.Models;
using Sarafaii.Services;

namespace Sarafaii.Controllers;

public class AccountController(IMapper mapper, ITokenService tokenService, UserManager<AppUser> userManager) : BaseApiController(mapper)
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerRequest)
    {
        if (await UserExists(registerRequest.Email))
            return BadRequest("Email Already Exists");

        var user = Mapper.Map<AppUser>(registerRequest);
        
        var result = await userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var roleResult = await userManager.AddToRoleAsync(user, "Member");
        
        if (!roleResult.Succeeded)
            return BadRequest(roleResult.Errors);

        var userToReturn = new UserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.Email,
            Token = await tokenService.CreateToken(user)
        };

        return Ok(userToReturn);
    }

    private async Task<bool> UserExists(string email)
    {
        return await userManager.Users.AnyAsync(x => x.UserName == email);
    }

    
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
    {
        if (!await UserExists(loginRequest.Email))
            return Unauthorized("Email Does Not Exist");

        var user = await userManager.Users.FirstOrDefaultAsync(user => user.UserName == loginRequest.Email);

        if (user == null)
            return Unauthorized("Password Is Wrong");
        
        var userToReturn = new UserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.Email,
            Token = await tokenService.CreateToken(user)
        };
        
        return Ok(userToReturn);
    }
}