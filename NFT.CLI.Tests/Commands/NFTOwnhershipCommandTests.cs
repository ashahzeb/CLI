using NFT.Application.Queries;
using NFT.Application.Responses;
using NFT.CLI.Commands;
using MediatR;
using Moq;
using Xunit;

namespace NFT.CLI.Tests.Commands;


public class NFTOwnershipCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public async Task PerformActionAsync_SuccessfulQuery_ReturnsTrue()
    {
        // Arrange
        var args = new string[] { "--nft", "nftId" };
        var queryResponse = new NFTOwnerResponse() { IsSuccess = true, Message = "Success message" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetNFTOwnerQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(queryResponse);

        var command = new NFTOwnershipCommand(args, _mediatorMock.Object);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task PerformActionAsync_FailedQuery_ReturnsFalse()
    {
        // Arrange
        var args = new string[] { "--nft", "nftId" };
        var queryResponse = new NFTOwnerResponse { IsSuccess = false, Message = "Error message" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetNFTOwnerQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(queryResponse);

        var command = new NFTOwnershipCommand(args, _mediatorMock.Object);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.False(result);
    }
}