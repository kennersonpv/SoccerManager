using SoccerManager.Api.Feature.Teams.UpdateTeam;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;
using SoccerManager.Api.Shared.Model;

namespace SoccerManager.Api.Feature.Transfers.TransferList
{
    public interface ITransferListUseCase
    {
        Task<Result<TransferListResponse>> Handle(CancellationToken cancellationToken);
    }

    public record TransferListResponse(IEnumerable<PlayerModel> listPlayers);
}
