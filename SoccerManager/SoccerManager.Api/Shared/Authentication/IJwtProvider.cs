using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Authentication
{
    public interface IJwtProvider
    {
        string GetJwtToken(string email);
    }
}
