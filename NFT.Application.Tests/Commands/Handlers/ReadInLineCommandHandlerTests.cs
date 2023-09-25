using Moq;
using Newtonsoft.Json;
using NFT.Application.Commands;
using NFT.Application.Commands.Handlers;
using NFT.Application.Domain;
using NFT.Application.Helpers;
using NFT.Application.Processors;
using Xunit;

namespace NFT.Application.Tests.Commands.Handlers;

public class ReadInLineCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidJson_ProcessesTransactions()
    {
        // Arrange
        var jsonHelperMock = new Mock<IJsonHelper<Transaction>>();
        var transactionProcessorMock = new Mock<ITransactionProcessor>();
        var handler = new ReadInLineCommandHandler(jsonHelperMock.Object, transactionProcessorMock.Object);
        var transactions = new List<Transaction> { new Transaction() };
        var jsonText = JsonConvert.SerializeObject(transactions);
        var request = new ReadInLineCommand(jsonText);

        // Configure mocks
        jsonHelperMock.Setup(h => h.IsJsonArray(jsonText)).Returns(true);
        jsonHelperMock.Setup(h => h.ProcessArray(jsonText)).Returns(transactions);
        transactionProcessorMock.Setup(p => p.Process(transactions)).Returns(Task.CompletedTask);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal($"Read {transactions.Count} transaction(s)", response.Message);
    }

    [Fact]
    public async Task Handle_InvalidJson_ReturnsInvalidJsonResponse()
    {
        // Arrange
        var jsonHelperMock = new Mock<IJsonHelper<Transaction>>();
        var transactionProcessorMock = new Mock<ITransactionProcessor>();
        var handler = new ReadInLineCommandHandler(jsonHelperMock.Object, transactionProcessorMock.Object);
        var invalidJsonText = "Invalid JSON";
        var request = new ReadInLineCommand(invalidJsonText);

        // Configure mocks
        jsonHelperMock.Setup(h => h.IsJsonArray(invalidJsonText)).Throws<JsonException>();

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal("Exception of type 'Newtonsoft.Json.JsonException' was thrown.", response.Message);
    }

    [Fact]
    public async Task Handle_EmptyJson_ReturnsEmptyJsonResponse()
    {
        // Arrange
        var jsonHelperMock = new Mock<IJsonHelper<Transaction>>();
        var transactionProcessorMock = new Mock<ITransactionProcessor>();
        var handler = new ReadInLineCommandHandler(jsonHelperMock.Object, transactionProcessorMock.Object);
        var emptyJsonText = "{}";
        var request = new ReadInLineCommand(emptyJsonText);

        // Configure mocks
        jsonHelperMock.Setup(h => h.IsJsonArray(emptyJsonText)).Returns(false);
        jsonHelperMock.Setup(h => h.ProcessSingleObject(emptyJsonText)).Returns((Transaction)null);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal("Read 0 transaction(s)", response.Message);
    }
}
