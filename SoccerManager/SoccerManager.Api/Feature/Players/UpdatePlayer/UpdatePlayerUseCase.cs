using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Repositories.Players;
using SoccerManager.Api.Shared.Repositories.TeamPlayers;
using SoccerManager.Api.Shared.Repositories.Teams;
using System.IdentityModel.Tokens.Jwt;

namespace SoccerManager.Api.Feature.Players.UpdatePlayer
{
    public class UpdatePlayerUseCase : IUpdatePlayerUseCase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly ITeamsRepository _teamsRepository;
        private readonly ITeamPlayersRepository _teamPlayersRepository;

        public UpdatePlayerUseCase(IPlayersRepository playersRepository, 
            ITeamsRepository teamsRepository, 
            ITeamPlayersRepository teamPlayersRepository)
        {
            _playersRepository = playersRepository;
            _teamsRepository = teamsRepository;
            _teamPlayersRepository = teamPlayersRepository;
        }

        public async Task<Result<UpdatePlayerResponse>> Handle(UpdatePlayerRequest request, string authorization, CancellationToken cancellationToken)
        {
            var bearerToken = authorization.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(bearerToken) as JwtSecurityToken;
            var email = jsonToken.Claims.Where(c => c.Type == "email").FirstOrDefault().Value;

            var teamResponse = await _teamsRepository.GetTeamByUserEmailAsync(email, cancellationToken);

            if(teamResponse.IsSuccess)
            {
                var teamPlayerResponse = await _teamPlayersRepository.GetPlayerByTeamAsync(teamResponse.Value.Id, 
                                                                                           request.Id,
                                                                                           cancellationToken);

                if (teamPlayerResponse.IsSuccess)
                {
                    var response = await _playersRepository.UpdatePlayerAsync(request.Id,
                        request.FirstName,
                        request.LastName,
                        request.PlayerCountry,
                        cancellationToken);

                    if (response.IsSuccess)
                    {
                        return Result<UpdatePlayerResponse>.Success(new UpdatePlayerResponse(true));
                    }
                }
                else
                {
                    return Result<UpdatePlayerResponse>.Failure(new Error("Player", "Player not found"));
                }
            }
            else
            {
                return Result<UpdatePlayerResponse>.Failure(new Error("Player", "Team not found"));
            }

            return Result<UpdatePlayerResponse>.Failure(new Error("Player", "Could not update player"));
        }
    }
}
