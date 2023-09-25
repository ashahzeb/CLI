using MediatR;
using NFT.Application.Responses;

namespace NFT.Application.Commands;

public record ReadFileCommand (string FileName) : IRequest<ReadFileResponse>;