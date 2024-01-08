using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;
using SoccerManager.Api.Shared.Enums;
using SoccerManager.Api.Shared.Helpers;
using SoccerManager.Api.Shared.Repositories.Players;
using SoccerManager.Api.Shared.Repositories.PlayerValues;
using SoccerManager.Api.Shared.Repositories.TeamAmounts;
using SoccerManager.Api.Shared.Repositories.TeamPlayers;
using SoccerManager.Api.Shared.Repositories.Teams;
using SoccerManager.Api.Shared.Repositories.Users;

namespace SoccerManager.Api.Feature.User.InsertUser
{
    public class InsertUserUseCase : IInsertUserUseCase
    {
        private const int MAX_GOALKEEPER = 3;
        private const int MAX_DEFENDER = 6;
        private const int MAX_MIDFIELDER = 6;
        private const int MAX_ATTACKER = 5;
        private const int INITIAL_PLAYER_VALUE = 1000;
        private const int TEAM_AMOUNT = 5000;
        private readonly IUsersRepository _usersRepository;
        private readonly ITeamsRepository _teamsRepository;
        private readonly IPlayersRepository _playersRepository;
        private readonly ITeamPlayersRepository _teamPlayersRepository;
        private readonly IPlayerValueRepository _playerValueRepository;
        private readonly ITeamAmountRepository _teamAmountRepository;

        public InsertUserUseCase(IUsersRepository usersRepository,
            ITeamsRepository teamsRepository,
            IPlayersRepository playersRepository,
            ITeamPlayersRepository teamPlayersRepository,
            IPlayerValueRepository playerValueRepository,
            ITeamAmountRepository teamAmountRepository)
        {
            _usersRepository = usersRepository;
            _teamsRepository = teamsRepository;
            _playersRepository = playersRepository;
            _teamPlayersRepository = teamPlayersRepository;
            _playerValueRepository = playerValueRepository;
            _teamAmountRepository = teamAmountRepository;
        }

        public async Task<Result<InsertUserResponse>> Handle(InsertUserRequest request, CancellationToken cancellationToken)
        {
            var responseUser = await _usersRepository.InsertUserAsync(request.user, cancellationToken);

            if (responseUser.IsSuccess)
            {
                var responseTeam = await _teamsRepository.AddTeamAsync(request.team, responseUser.Value, cancellationToken);
                var responseTeamAmount = await _teamAmountRepository.AddTeamAmountAsync(responseTeam.Value, TEAM_AMOUNT, cancellationToken);

                if (responseTeam.IsSuccess)
                {
                    await GeneratePlayersAsync(PositionEnum.Goalkeeper, MAX_GOALKEEPER, responseTeam.Value, cancellationToken);
                    await GeneratePlayersAsync(PositionEnum.Defender, MAX_DEFENDER, responseTeam.Value, cancellationToken);
                    await GeneratePlayersAsync(PositionEnum.Midfielder, MAX_MIDFIELDER, responseTeam.Value, cancellationToken);
                    await GeneratePlayersAsync(PositionEnum.Attacker, MAX_ATTACKER, responseTeam.Value, cancellationToken);
                }
                else
                {
                    return Result<InsertUserResponse>.Failure(new Error("Team", "Error on add Team"));
                }
            }
            else
            {
                return Result<InsertUserResponse>.Failure(new Error("User", "Error on add User"));
            }

            return Result<InsertUserResponse>.Success(new InsertUserResponse(responseUser.Value));
        }

        private async Task GeneratePlayersAsync(PositionEnum position, int count, int teamId, CancellationToken cancellationToken)
        {
            for (var i = 0; i < count; i++)
            {
                var player = new Player()
                {
                    Age = AgeHelper.RandomAge(),
                    Country = CountryHelper.RandomCountry(),
                    FirstName = PlayerHelper.RandomFirstNamePlayer(),
                    LastName = PlayerHelper.RandomLastNamePlayer(),
                    Position = position.ToString()
                };

                var responsePlayer = await _playersRepository.AddPlayerAsync(player, cancellationToken);
                if (responsePlayer.IsSuccess)
                {
                    await _playerValueRepository.AddPlayerValueAsync(responsePlayer.Value, INITIAL_PLAYER_VALUE, cancellationToken);
                    await _teamPlayersRepository.AddTeamPlayerAsync(responsePlayer.Value, teamId, cancellationToken);
                }
            }
        }
    }
}
