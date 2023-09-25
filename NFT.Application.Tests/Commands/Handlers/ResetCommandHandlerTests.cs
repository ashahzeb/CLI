using System.Threading;
using System.Threading.Tasks;
using Moq;
using NFT.Application.Commands;
using NFT.Application.Commands.Handlers;
using NFT.Application.Domain.Repositories;
using Xunit;

namespace NFT.Application.Tests.Commands.Handlers;

public class ResetCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeletesAllTransactions_ReturnsSuccessMessage()
    {
        // Arrange
        var repositoryMock = new Mock<ITransactionRepository>();
        var handler = new ResetCommandHandler(repositoryMock.Object);
        var request = new ResetCommand();

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        repositoryMock.Verify(r => r.DeleteAll(), Times.Once);
        Assert.Equal("Program was reset", response);
    }
}
