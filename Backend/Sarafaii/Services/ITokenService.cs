using Sarafaii.Models;

namespace Sarafaii.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}