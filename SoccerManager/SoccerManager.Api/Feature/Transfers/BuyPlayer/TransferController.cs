using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerManager.Api.Feature.Transfers.SellPlayer;

namespace SoccerManager.Api.Feature.Transfers.BuyPlayer
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IBuyPlayerUseCase _buyPlayerUseCase;
        public TransferController(IBuyPlayerUseCase buyPlayerUseCase)
        {
            _buyPlayerUseCase = buyPlayerUseCase;
        }

        [Authorize]
        [HttpPost("sell")]
        public async Task<BuyPlayerResponse> BuyPlayer(
                    [FromBody] BuyPlayerRequest request,
                    CancellationToken cancellationToken)
        {
            var authorization = HttpContext.Request.Headers["Authorization"];

            var response = await _buyPlayerUseCase.Handle(request, authorization, cancellationToken);
            return response.Value;
        }
    }
}
