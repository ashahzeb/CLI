using MediatR;
using NFT.Application.Domain.Repositories;

namespace NFT.Application.Commands.Handlers;

public class ResetCommandHandler : IRequestHandler<ResetCommand, string>
{
    private readonly ITransactionRepository _repository;

    public ResetCommandHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(ResetCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAll();
        return "Program was reset";
    }
}