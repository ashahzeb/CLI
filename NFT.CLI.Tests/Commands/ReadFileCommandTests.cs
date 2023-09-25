using MediatR;
using Moq;
using NFT.Application.Responses;
using NFT.CLI.Commands;
using Xunit;

namespace NFT.CLI.Tests.Commands;


public class ReadFileCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public async Task PerformActionAsync_SuccessfulCommand_ReturnsTrue()
    {
        // Arrange
        var args = new string[] { "--read-file", "file-path" };
        var commandResponse = new ReadFileResponse { IsSuccess = true, Message = "Success message" };
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<NFT.Application.Commands.ReadFileCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

        var command = new ReadFileCommand(args, _mediatorMock.Object);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task PerformActionAsync_FailedCommand_ReturnsFalse()
    {
        // Arrange
        var args = new string[] { "--read-file", "file-path" };
        var commandResponse = new ReadFileResponse { IsSuccess = false, Message = "Error message" };
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<NFT.Application.Commands.ReadFileCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

        var command = new ReadFileCommand(args, _mediatorMock.Object);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.False(result);
    }
}

