namespace SoccerManager.Api.Feature.Auth
{
    public interface IAuthUseCase
    {
        Task<AuthResponse> Handle(AuthRequest request, CancellationToken cancellationToken);
    }
    public record AuthRequest(string Email, string Password);
    public record AuthResponse(string Token);
}
