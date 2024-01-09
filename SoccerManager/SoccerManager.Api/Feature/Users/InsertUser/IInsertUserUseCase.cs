using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Feature.User.InsertUser
{
    public interface IInsertUserUseCase
    {
        Task<Result<InsertUserResponse>> Handle(InsertUserRequest request, CancellationToken cancellationToken);
    }

    public record InsertUserRequest(UserRequest User);
    public record InsertUserResponse(int userId);

    public record UserRequest(string Name, string Email, string Password, TeamRequest Team);
    public record TeamRequest(string Name, string Country);
}
