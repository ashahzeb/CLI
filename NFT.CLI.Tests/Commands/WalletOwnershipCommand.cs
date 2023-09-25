using NFT.Application.Queries;
using NFT.Application.Responses;
using NFT.CLI.Commands;
using MediatR;
using Moq;
using Xunit;

namespace NFT.CLI.Tests.Commands;

public class WalletOwnershipCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public async Task PerformActionAsync_SuccessfulQuery_ReturnsTrue()
    {
        // Arrange
        var args = new string[] { "--wallet", "wallet-address" };
        var queryResponse = new WalletOwnershipResponse
        {
            IsSuccess = true,
            Message = "Success message",
            Tokens = new[] { "Token1", "Token2" }
        };
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWalletOwnershipQuery>(), default)).ReturnsAsync(queryResponse);

        var command = new WalletOwnershipCommand(args, _mediatorMock.Object);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task PerformActionAsync_FailedQuery_ReturnsFalse()
    {
        // Arrange
        var args = new string[] { "--wallet", "wallet-address" };
        var queryResponse = new WalletOwnershipResponse
        {
            IsSuccess = false,
            Message = "Error message"
        };
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWalletOwnershipQuery>(), default)).ReturnsAsync(queryResponse);

        var command = new WalletOwnershipCommand(args, _mediatorMock.Object);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.False(result);
    }
}
