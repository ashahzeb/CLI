using NFT.Application.Commands.Handlers;
using NFT.Application.Domain;
using Moq;
using Newtonsoft.Json;
using NFT.Application.Commands;
using NFT.Application.Helpers;
using NFT.Application.Processors;
using Xunit;

namespace NFT.Application.Tests.Commands.Handlers;

public class ReadFileCommandHandlerTests
{
    [Fact]
    public async Task Handle_FileNotFound_ReturnsFileNotFoundResponse()
    {
        // Arrange
        var jsonHelperMock = new Mock<IJsonHelper<Transaction>>();
        var transactionProcessorMock = new Mock<ITransactionProcessor>();
        var handler = new ReadFileCommandHandler(jsonHelperMock.Object, transactionProcessorMock.Object);
        var request = new ReadFileCommand("nonexistentfile.json");

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal("File not found", response.Message);
    }

    [Fact]
    public async Task Handle_ValidFile_ProcessesTransactions()
    {
        // Arrange
        var jsonHelperMock = new Mock<IJsonHelper<Transaction>>();
        var transactionProcessorMock = new Mock<ITransactionProcessor>();
        var handler = new ReadFileCommandHandler(jsonHelperMock.Object, transactionProcessorMock.Object);
        var transactions = new List<Transaction> { new Transaction() };
        var jsonText = JsonConvert.SerializeObject(transactions);
        var tempFilePath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(tempFilePath, jsonText);
            var request = new ReadFileCommand(tempFilePath);

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
        finally
        {
            File.Delete(tempFilePath);
        }
    }

    [Fact]
    public async Task Handle_InvalidJson_ReturnsInvalidJsonResponse()
    {
        // Arrange
        var jsonHelperMock = new Mock<IJsonHelper<Transaction>>();
        var transactionProcessorMock = new Mock<ITransactionProcessor>();
        var handler = new ReadFileCommandHandler(jsonHelperMock.Object, transactionProcessorMock.Object);
        var invalidJsonText = "Invalid JSON";
        var tempFilePath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(tempFilePath, invalidJsonText);
            var request = new ReadFileCommand(tempFilePath);

            // Configure mocks
            jsonHelperMock.Setup(h => h.IsJsonArray(invalidJsonText)).Throws<JsonException>();

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Invalid JSON format", response.Message);
        }
        finally
        {
            File.Delete(tempFilePath);
        }
    }
}
