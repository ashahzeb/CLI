using MediatR;
using NFT.Application.Domain;
using Moq;
using NFT.Application.Events;
using NFT.Application.Processors;
using Xunit;

namespace NFT.Application.Tests.Processors;

public class TransactionProcessorTests
{
    [Fact]
    public async Task Process_MintTransaction_PublishesMintEvent()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var processor = new TransactionProcessor(mediatorMock.Object);
        var mintTransaction = new Transaction { Type = TransactionType.Mint };

        // Act
        await processor.Process(new List<Transaction> { mintTransaction });

        // Assert
        mediatorMock.Verify(m => m.Publish<INotification>(It.IsAny<MintEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Process_BurnTransaction_PublishesBurnEvent()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var processor = new TransactionProcessor(mediatorMock.Object);
        var burnTransaction = new Transaction { Type = TransactionType.Burn };

        // Act
        await processor.Process(new List<Transaction> { burnTransaction });

        // Assert
        mediatorMock.Verify(m => m.Publish<INotification>(It.IsAny<BurnEvent>(), default), Times.Once);
    }

    [Fact]
    public async Task Process_TransferTransaction_PublishesTransferEvent()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var processor = new TransactionProcessor(mediatorMock.Object);
        var transferTransaction = new Transaction { Type = TransactionType.Transfer };

        // Act
        await processor.Process(new List<Transaction> { transferTransaction });

        // Assert
        mediatorMock.Verify(m => m.Publish<INotification>(It.IsAny<TransferEvent>(), default), Times.Once);
    }

    [Fact]
    public async Task Process_InvalidTransaction_ThrowsException()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var processor = new TransactionProcessor(mediatorMock.Object);
        var invalidTransaction = new Transaction { Type = (TransactionType)99 }; // Invalid type

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            processor.Process(new List<Transaction> { invalidTransaction }));
    }
}