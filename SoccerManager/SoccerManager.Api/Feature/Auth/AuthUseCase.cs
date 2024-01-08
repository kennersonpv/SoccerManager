using SoccerManager.Api.Shared.Authentication;
using SoccerManager.Api.Shared.Repositories.Users;

namespace SoccerManager.Api.Feature.Auth
{
    public class AuthUseCase : IAuthUseCase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthUseCase(IUsersRepository usersRepository, IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<AuthResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            var response = await _usersRepository.UserExistsByEmailPasswordAsync(request.Email, request.Password, cancellationToken);

            if (response.IsSuccess)
            {
                var token = _jwtProvider.GetJwtToken(request.Email);
                return new AuthResponse(token);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
