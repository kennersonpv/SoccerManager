using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoccerManager.Api.Feature.Transfers.SellPlayer
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ISellPlayerUseCase _sellPlayerUseCase;
        public TransferController(ISellPlayerUseCase sellPlayerUseCase)
        {
            _sellPlayerUseCase = sellPlayerUseCase;
        }

        [Authorize]
        [HttpPost("buy")]
        public async Task<SellPlayerResponse> SellPlayer(
            [FromBody] SellPlayerRequest request,
            CancellationToken cancellationToken)
        {
            var authorization = HttpContext.Request.Headers["Authorization"];

            var response = await _sellPlayerUseCase.Handle(request, authorization, cancellationToken);
            return response.Value;
        }
    }
}
