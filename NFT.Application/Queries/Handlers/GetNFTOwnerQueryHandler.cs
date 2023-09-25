using MediatR;
using NFT.Application.Domain.Repositories;
using NFT.Application.Responses;

namespace NFT.Application.Queries.Handlers;

public class GetNFTOwnerQueryHandler : IRequestHandler<GetNFTOwnerQuery, NFTOwnerResponse>
{
    private readonly ITransactionRepository _repository;

    public GetNFTOwnerQueryHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<NFTOwnerResponse> Handle(GetNFTOwnerQuery request, CancellationToken cancellationToken)
    {
        var response = new NFTOwnerResponse();

        var result = await _repository.GetByIdAsync(request.tokenId);

        if (string.IsNullOrEmpty(result))
        {
            response.IsSuccess = false;
            response.Message = $"{request.tokenId} is not owned by any wallet";

            return response;
        }

        response.IsSuccess = true;
        response.Message = $"{request.tokenId} is owned by {result}";
        
        return response;
    }
}