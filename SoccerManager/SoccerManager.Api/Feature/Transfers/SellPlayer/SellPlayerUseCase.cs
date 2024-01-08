using SoccerManager.Api.Feature.Teams.UpdateTeam;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Repositories.Teams;
using SoccerManager.Api.Shared.Repositories.Transfers;
using System.IdentityModel.Tokens.Jwt;

namespace SoccerManager.Api.Feature.Transfers.SellPlayer
{
    public class SellPlayerUseCase : ISellPlayerUseCase
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ITeamsRepository _teamsRepository;

        public SellPlayerUseCase(ITransferRepository transferRepository, ITeamsRepository teamsRepository)
        {
            _transferRepository = transferRepository;
            _teamsRepository = teamsRepository;
        }

        public async Task<Result<SellPlayerResponse>> Handle(SellPlayerRequest request, string authorization, CancellationToken cancellationToken)
        {
            var bearerToken = authorization.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(bearerToken) as JwtSecurityToken;
            var email = jsonToken.Claims.Where(c => c.Type == "email").FirstOrDefault().Value;

            var teamResponse = await _teamsRepository.GetTeamByUserEmailAsync(email, cancellationToken);

            if (teamResponse.IsSuccess)
            {
                var response = await _transferRepository.AddTransferAsync(request.playerId, request.value, teamResponse.Value.Id, cancellationToken);

                if (response.IsSuccess)
                {
                    return Result<SellPlayerResponse>.Success(new SellPlayerResponse());
                }
            }

            return Result<SellPlayerResponse>.Failure(new Error("Transfer", "Error to sell player"));
        }
    }
}
