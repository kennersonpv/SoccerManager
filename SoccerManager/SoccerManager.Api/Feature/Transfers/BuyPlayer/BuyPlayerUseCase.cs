using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Helpers;
using SoccerManager.Api.Shared.Repositories.PlayerValues;
using SoccerManager.Api.Shared.Repositories.TeamAmounts;
using SoccerManager.Api.Shared.Repositories.TeamPlayers;
using SoccerManager.Api.Shared.Repositories.Teams;
using SoccerManager.Api.Shared.Repositories.Transfers;
using System.IdentityModel.Tokens.Jwt;

namespace SoccerManager.Api.Feature.Transfers.BuyPlayer
{
    public class BuyPlayerUseCase : IBuyPlayerUseCase
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ITeamsRepository _teamsRepository;
        private readonly ITeamAmountRepository _teamAmountRepository;
        private readonly IPlayerValueRepository _playerValueRepository;
        private readonly ITeamPlayersRepository _teamPlayersRepository;

        public BuyPlayerUseCase(ITransferRepository transferRepository,
            ITeamsRepository teamsRepository,
            ITeamAmountRepository teamAmountRepository,
            IPlayerValueRepository playerValueRepository,
            ITeamPlayersRepository teamPlayersRepository)
        {
            _transferRepository = transferRepository;
            _teamsRepository = teamsRepository;
            _teamAmountRepository = teamAmountRepository;
            _playerValueRepository = playerValueRepository;
            _teamPlayersRepository = teamPlayersRepository;
        }

        public async Task<Result<BuyPlayerResponse>> Handle(BuyPlayerRequest request, string authorization, CancellationToken cancellationToken)
        {
            var bearerToken = authorization.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(bearerToken) as JwtSecurityToken;
            var email = jsonToken.Claims.Where(c => c.Type == "email").FirstOrDefault().Value;

            var teamResponse = await _teamsRepository.GetTeamByUserEmailAsync(email, cancellationToken);

            if (teamResponse.IsSuccess)
            {
                var responsePlayerValue = await _transferRepository.GetPlayerValueAsync(request.playerId, cancellationToken);
                if (responsePlayerValue.IsSuccess)
                {
                    var responseTeamAmount = await _teamAmountRepository.GetTeamAmountAsync(teamResponse.Value.Id, cancellationToken);

                    if (responseTeamAmount.IsSuccess)
                    {
                        if (responseTeamAmount.Value >= responsePlayerValue.Value)
                        {
                            var response = await _transferRepository.UpdateTransferAsync(request.playerId, teamResponse.Value.Id, cancellationToken);

                            if (response.IsSuccess)
                            {
                                await _teamAmountRepository.AddTeamAmountAsync(teamResponse.Value.Id, 
                                                                                responseTeamAmount.Value - responsePlayerValue.Value, 
                                                                                cancellationToken);

                                await _playerValueRepository.AddPlayerValueAsync(request.playerId,
                                                                                PlayerHelper.RandomNewValue(responsePlayerValue.Value),
                                                                                cancellationToken);

                                await _teamPlayersRepository.UpdateTeamPlayerAsync(request.playerId, cancellationToken);

                                await _teamPlayersRepository.AddTeamPlayerAsync(request.playerId, teamResponse.Value.Id, cancellationToken);

                                return Result<BuyPlayerResponse>.Success(new BuyPlayerResponse());
                            }
                        }
                    }
                }
            }

            return Result<BuyPlayerResponse>.Failure(new Error("Transfer", "Error to buy player"));
        }
    }
}
