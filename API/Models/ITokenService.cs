namespace API.Models;

public interface ITokenService
{
    string CreateToken(AppUser user);
}