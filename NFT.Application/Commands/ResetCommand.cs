using MediatR;

namespace NFT.Application.Commands;

public record ResetCommand : IRequest<string>;