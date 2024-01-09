using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerManager.Api.Feature.Teams.UpdateTeam;

namespace SoccerManager.Api.Feature.Transfers.TransferList
{
    [Route("api/Transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferListUseCase _transferListUseCase;
        public TransferController(ITransferListUseCase transferListUseCase)
        {
            _transferListUseCase = transferListUseCase;
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<TransferListResponse> GetTransferList(CancellationToken cancellationToken)
        {
            var authorization = HttpContext.Request.Headers["Authorization"];

            var response = await _transferListUseCase.Handle(cancellationToken);
            return response.Value;
        }
    }
}
