using MediatR;
using NFT.Application.Responses;

namespace NFT.Application.Queries;

public record GetNFTOwnerQuery(string tokenId) : IRequest<NFTOwnerResponse>;
