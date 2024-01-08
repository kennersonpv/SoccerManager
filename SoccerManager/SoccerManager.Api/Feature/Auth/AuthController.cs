using Microsoft.AspNetCore.Mvc;
using SoccerManager.Api.Feature.Auth;

namespace SoccerManager.Api.UseCases.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUseCase _authUseCase;
        private readonly CancellationToken _cancellationToken;
        public AuthController(IAuthUseCase authUseCase)
        {
            _authUseCase = authUseCase;
            _cancellationToken = new CancellationToken();
        }

        [HttpPost]
        public async Task<AuthResponse> Auth([FromBody] AuthRequest request)
        {
            return await _authUseCase.Handle(request, _cancellationToken);
        }
    }
}
