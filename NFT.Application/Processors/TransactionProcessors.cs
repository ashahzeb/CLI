using MediatR;
using NFT.Application.Domain;
using NFT.Application.Events;

namespace NFT.Application.Processors;

public interface ITransactionProcessor
{
    Task Process(IEnumerable<Transaction> transactions);
}

public class TransactionProcessor : ITransactionProcessor
{
    private readonly IMediator _mediator;

    public TransactionProcessor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Process(IEnumerable<Transaction> transactions)
    {
        foreach (var transaction in transactions)
        {
            INotification transactionEvent = transaction.Type switch
            {
                TransactionType.Mint => new MintEvent(transaction),
                TransactionType.Burn => new BurnEvent(transaction),
                TransactionType.Transfer => new TransferEvent(transaction),
                _ => throw new ArgumentOutOfRangeException(nameof(transaction.Type), $"Invalid Transaction Type {transaction.Type}")
            };

            await _mediator.Publish(transactionEvent);
        }
    }
}