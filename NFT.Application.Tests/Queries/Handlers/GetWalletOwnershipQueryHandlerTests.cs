using Moq;
using NFT.Application.Domain.Repositories;
using NFT.Application.Queries;
using NFT.Application.Queries.Handlers;
using Xunit;


namespace NFT.Application.Tests.Queries.Handlers;


public class GetWalletOwnershipQueryHandlerTests
{
    [Fact]
    public async Task Handle_WalletWithTokens_ReturnsWalletOwnershipResponse()
    {
        // Arrange
        var address = "wallet1";
        var tokens = new List<string> { "token1", "token2" };
        var repositoryMock = new Mock<ITransactionRepository>();
        repositoryMock.Setup(r => r.GetByAddressAsync(address)).ReturnsAsync(tokens);
        var handler = new GetWalletOwnershipQueryHandler(repositoryMock.Object);
        var query = new GetWalletOwnershipQuery(address);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal($"{address} holds {tokens.Count} Token(s)", response.Message);
        Assert.Equal(tokens, response.Tokens);
    }

    [Fact]
    public async Task Handle_WalletWithNoTokens_ReturnsWalletOwnershipResponse()
    {
        // Arrange
        var address = "wallet2";
        var tokens = new List<string>();
        var repositoryMock = new Mock<ITransactionRepository>();
        repositoryMock.Setup(r => r.GetByAddressAsync(address)).ReturnsAsync(tokens);
        var handler = new GetWalletOwnershipQueryHandler(repositoryMock.Object);
        var query = new GetWalletOwnershipQuery(address);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal($"{address} holds no Token", response.Message);
        Assert.Empty(response.Tokens);
    }

    [Fact]
    public async Task Handle_EmptyAddress_ReturnsWalletOwnershipResponse()
    {
        // Arrange
        var address = string.Empty;
        var tokens = new List<string>();
        var repositoryMock = new Mock<ITransactionRepository>();
        var handler = new GetWalletOwnershipQueryHandler(repositoryMock.Object);
        var query = new GetWalletOwnershipQuery(address);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal($"{address} holds no Token", response.Message);
        Assert.Empty(response.Tokens);
    }
}
