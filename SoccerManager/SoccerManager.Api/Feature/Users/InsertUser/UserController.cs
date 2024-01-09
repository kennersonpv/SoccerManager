using Microsoft.AspNetCore.Mvc;

namespace SoccerManager.Api.Feature.User.InsertUser
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IInsertUserUseCase _userUseCase;
        public UserController(IInsertUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpPost("Create")]
        public async Task<InsertUserResponse> CreateUser(
            [FromBody] InsertUserRequest request,
            CancellationToken cancellationToken
            )
        {
            var response = await _userUseCase.Handle(request, cancellationToken);
            return response.Value;
        }
    }
}
