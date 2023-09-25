using Moq;
using NFT.Application.Domain;
using NFT.Application.Domain.Repositories;
using NFT.Application.Events;
using NFT.Application.Events.Handlers;
using Xunit;

namespace NFT.Application.Tests.Events.Handlers;

public class TransactionEventHandlerTests
{
    [Fact]
    public async Task Handle_MintEvent_AddsTransaction()
    {
        // Arrange
        var repositoryMock = new Mock<ITransactionRepository>();
        var handler = new TransactionEventHandler(repositoryMock.Object);
        var mintEvent = new MintEvent ( new Transaction() );

        // Act
        await handler.Handle(mintEvent, CancellationToken.None);

        // Assert
        repositoryMock.Verify(r => r.AddAsync(mintEvent.Transaction), Times.Once);
    }

    [Fact]
    public async Task Handle_BurnEvent_DeletesTransaction()
    {
        // Arrange
        var repositoryMock = new Mock<ITransactionRepository>();
        var handler = new TransactionEventHandler(repositoryMock.Object);
        var burnEvent = new BurnEvent ( new Transaction() );

        // Act
        await handler.Handle(burnEvent, CancellationToken.None);

        // Assert
        repositoryMock.Verify(r => r.DeleteAsync(burnEvent.Transaction), Times.Once);
    }

    [Fact]
    public async Task Handle_TransferEvent_UpdatesTransaction()
    {
        // Arrange
        var repositoryMock = new Mock<ITransactionRepository>();
        var handler = new TransactionEventHandler(repositoryMock.Object);
        var transferEvent = new TransferEvent ( new Transaction() );

        // Act
        await handler.Handle(transferEvent, CancellationToken.None);

        // Assert
        repositoryMock.Verify(r => r.UpdateAsync(transferEvent.Transaction), Times.Once);
    }
}
