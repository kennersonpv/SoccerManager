using SoccerManager.Api.Shared.Abstractions;

namespace SoccerManager.Api.Feature.Transfers.SellPlayer
{
    public interface ISellPlayerUseCase
    {
        Task<Result<SellPlayerResponse>> Handle(SellPlayerRequest request, string authorization, CancellationToken cancellationToken);
    }

    public record SellPlayerRequest(int playerId, long value);
    public record SellPlayerResponse();
}
