using MediatR;
using NFT.Application.Responses;

namespace NFT.Application.Commands;

public record ReadInLineCommand (string Json) : IRequest<ReadInLineResponse>;