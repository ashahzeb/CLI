using MediatR;
using NFT.Application.Domain.Repositories;

namespace NFT.Application.Events.Handlers;

public class TransactionEventHandler : INotificationHandler<MintEvent>, 
    INotificationHandler<BurnEvent>,
    INotificationHandler<TransferEvent>
{
    private readonly ITransactionRepository _repository;

    public TransactionEventHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(MintEvent notification, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(notification.Transaction);
    }

    public async Task Handle(BurnEvent notification, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(notification.Transaction);
    }

    public async Task Handle(TransferEvent notification, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(notification.Transaction);
    }
}