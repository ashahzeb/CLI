using Moq;
using NFT.Application.Domain.Repositories;
using NFT.Application.Queries;
using NFT.Application.Queries.Handlers;
using Xunit;

namespace NFT.Application.Tests.Queries.Handlers;

public class GetNFTOwnerQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingTokenId_ReturnsOwnerResponse()
    {
        // Arrange
        var tokenId = "token1";
        var owner = "owner1";
        var repositoryMock = new Mock<ITransactionRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(tokenId)).ReturnsAsync(owner);
        var handler = new GetNFTOwnerQueryHandler(repositoryMock.Object);
        var query = new GetNFTOwnerQuery(tokenId);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal($"{tokenId} is owned by {owner}", response.Message);
    }

    [Fact]
    public async Task Handle_NonExistingTokenId_ReturnsOwnerResponse()
    {
        // Arrange
        var tokenId = "nonExistingToken";
        var repositoryMock = new Mock<ITransactionRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(tokenId)).ReturnsAsync(string.Empty);
        var handler = new GetNFTOwnerQueryHandler(repositoryMock.Object);
        var query = new GetNFTOwnerQuery(tokenId);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal($"{tokenId} is not owned by any wallet", response.Message);
    }

    [Fact]
    public async Task Handle_EmptyTokenId_ReturnsOwnerResponse()
    {
        // Arrange
        var tokenId = string.Empty;
        var repositoryMock = new Mock<ITransactionRepository>();
        var handler = new GetNFTOwnerQueryHandler(repositoryMock.Object);
        var query = new GetNFTOwnerQuery(tokenId);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal($"{tokenId} is not owned by any wallet", response.Message);
    }
}
