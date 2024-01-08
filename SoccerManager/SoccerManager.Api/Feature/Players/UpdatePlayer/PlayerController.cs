using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoccerManager.Api.Feature.Players.UpdatePlayer
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IUpdatePlayerUseCase _updatePlayerUseCase;
        public PlayerController(IUpdatePlayerUseCase updatePlayerUseCase)
        {
            _updatePlayerUseCase = updatePlayerUseCase;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<UpdatePlayerResponse> UpdatePlayer(
                            [FromBody] UpdatePlayerRequest request,
                            CancellationToken cancellationToken
                            )
        {
            var authorization = HttpContext.Request.Headers["Authorization"];

            var response = await _updatePlayerUseCase.Handle(request, authorization, cancellationToken);
            return response.Value;
        }
    }
}
