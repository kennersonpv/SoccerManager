using SoccerManager.Api.Shared.Abstractions;

namespace SoccerManager.Api.Feature.Teams.UpdateTeam
{
    public interface IUpdateTeamUseCase
    {
        Task<Result<UpdateTeamResponse>> Handle(UpdateTeamRequest request, string authorization, CancellationToken cancellationToken);
    }

    public record UpdateTeamRequest(string Name, string Country);
    public record UpdateTeamResponse(bool Updated);
}
