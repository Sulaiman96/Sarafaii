using Sarafaii.Models;

namespace Sarafaii.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}