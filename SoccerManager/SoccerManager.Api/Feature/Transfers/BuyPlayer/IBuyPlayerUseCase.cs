using SoccerManager.Api.Feature.Transfers.SellPlayer;
using SoccerManager.Api.Shared.Abstractions;

namespace SoccerManager.Api.Feature.Transfers.BuyPlayer
{
    public interface IBuyPlayerUseCase
    {
        Task<Result<BuyPlayerResponse>> Handle(BuyPlayerRequest request, string authorization, CancellationToken cancellationToken);
    }

    public record BuyPlayerRequest(int playerId);
    public record BuyPlayerResponse();
}
