using NFT.CLI.Commands;

namespace NFT.CLI.Tests.Commands;

using System;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NFT.CLI.Tools.Commands;
using Xunit;

public class ResetCommandTests
{
    private readonly Mock<IMediator> _mediatorMock = new();

    [Fact]
    public async Task PerformActionAsync_ReturnsTrue()
    {
        // Arrange
        var args = new string[] { "--reset" };

        var commandResponse = "Reset command response"; // Modify this based on your actual response type
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<NFT.Application.Commands.ResetCommand>(), default)).ReturnsAsync(commandResponse);

        var command = new ResetCommand(args, _mediatorMock.Object);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.True(result);
    }
}
