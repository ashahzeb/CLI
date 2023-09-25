using MediatR;
using NFT.Application.Responses;

namespace NFT.Application.Queries;

public record GetWalletOwnershipQuery(string address) : IRequest<WalletOwnershipResponse>;