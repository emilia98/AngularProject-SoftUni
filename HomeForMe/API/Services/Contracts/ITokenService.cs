using API.Entities;

namespace API.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}