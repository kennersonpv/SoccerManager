using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Feature.User.InsertUser
{
    public interface IInsertUserUseCase
    {
        Task<Result<InsertUserResponse>> Handle(InsertUserRequest request, CancellationToken cancellationToken);
    }

    public record InsertUserRequest(Shared.Entities.User user, Team team);
    public record InsertUserResponse(int userId);
}
