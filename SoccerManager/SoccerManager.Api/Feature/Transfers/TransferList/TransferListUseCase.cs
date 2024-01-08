using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Repositories.Transfers;

namespace SoccerManager.Api.Feature.Transfers.TransferList
{
    public class TransferListUseCase : ITransferListUseCase
    {
        private readonly ITransferRepository _transferRepository;

        public TransferListUseCase(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public async Task<Result<TransferListResponse>> Handle(CancellationToken cancellationToken)
        {
            var result = await _transferRepository.GetTransferListAsync();

            if (result.IsSuccess)
            {
                return Result<TransferListResponse>.Success(new TransferListResponse(result.Value));
            }
            else
            {
                return Result<TransferListResponse>.Failure(new Error("TransferList", "Error to get transfer list"));
            }
        }
    }
}
