using MediatR;
using NFT.Application.Domain.Repositories;
using NFT.Application.Responses;

namespace NFT.Application.Queries.Handlers;

public class GetWalletOwnershipQueryHandler : IRequestHandler<GetWalletOwnershipQuery, WalletOwnershipResponse>
{
    private readonly ITransactionRepository _repository;

    public GetWalletOwnershipQueryHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<WalletOwnershipResponse> Handle(GetWalletOwnershipQuery request, CancellationToken cancellationToken)
    {
        var response = new WalletOwnershipResponse();

        var result = await _repository.GetByAddressAsync(request.address);

        if (result.Any())
        {
            response.IsSuccess = true;
            response.Message = $"{request.address} holds {result.Count()} Token(s)";
            response.Tokens = result;

            return response;
        }

        response.IsSuccess = false;
        response.Message = $"{request.address} holds no Token";
        response.Tokens = result;
        
        return response;
    }
}