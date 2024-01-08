using SoccerManager.Api.Feature.Players.UpdatePlayer;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Repositories.Teams;
using System.IdentityModel.Tokens.Jwt;

namespace SoccerManager.Api.Feature.Teams.UpdateTeam
{
    public class UpdateTeamUseCase : IUpdateTeamUseCase
    {
        private readonly ITeamsRepository _teamsRepository;
        public UpdateTeamUseCase(ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
        }

        public async Task<Result<UpdateTeamResponse>> Handle(UpdateTeamRequest request, string authorization, CancellationToken cancellationToken)
        {
            var bearerToken = authorization.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(bearerToken) as JwtSecurityToken;
            var email = jsonToken.Claims.Where(c => c.Type == "email").FirstOrDefault().Value;

            var teamResponse = await _teamsRepository.GetTeamByUserEmailAsync(email, cancellationToken);

            if (teamResponse.IsSuccess)
            {
                var response = await _teamsRepository.UpdateTeamAsync(teamResponse.Value.Id, request.Name, request.Country, cancellationToken);

                if (response.IsSuccess)
                {
                    return Result<UpdateTeamResponse>.Success(new UpdateTeamResponse(true));
                }
            }
            else
            {
                return Result<UpdateTeamResponse>.Failure(new Error("Team", "Team not found"));
            }

            return Result<UpdateTeamResponse>.Failure(new Error("Team", "Error to update team"));
        }
    }
}
