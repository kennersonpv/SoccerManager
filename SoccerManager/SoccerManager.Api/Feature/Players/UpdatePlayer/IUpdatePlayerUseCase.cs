using SoccerManager.Api.Shared.Abstractions;

namespace SoccerManager.Api.Feature.Players.UpdatePlayer
{
    public interface IUpdatePlayerUseCase
    {
        Task<Result<UpdatePlayerResponse>> Handle(UpdatePlayerRequest request, string authorization, CancellationToken cancellationToken);
    }

    public record UpdatePlayerRequest(int Id, string FirstName, string LastName, string PlayerCountry);
    public record UpdatePlayerResponse(bool updated);
}
