namespace NFT.CLI.Tests.Tools.Commands;

using System;
using MediatR;
using Moq;
using NFT.CLI.Tools.Commands;
using Xunit;

public class CommandInfoTests
{
    private readonly Mock<IMediator> mediatorMock;

    [Fact]
    public void CreateCommand_ValidConstructor_ReturnsInstanceOfT()
    {
        // Arrange
        var commandName = "SampleCommand";
        var commandInfo = new CommandInfo<SampleCommand>(commandName, mediatorMock.Object);
        var args = new string[] { "arg1", "arg2" };

        // Act
        var command = commandInfo.CreateCommand(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<SampleCommand>(command);
    }

    [Fact]
    public void CreateCommand_MissingConstructor_ThrowsException()
    {
        // Arrange
        var commandName = "InvalidCommand";
        var commandInfo = new CommandInfo<InvalidCommand>(commandName, mediatorMock.Object);
        var args = new string[] { "arg1", "arg2" };

        // Act and Assert
        Assert.Throws<Exception>(() => commandInfo.CreateCommand(args));
    }
}

public class SampleCommand : ICommand
{
    public SampleCommand(string[] args, IMediator mediator)
    {
        // Command constructor logic
    }

    public async Task<bool> ExecuteAsync()
    {
        return true;
    }
}

public class InvalidCommand : ICommand
{
    // Missing required constructor
    public InvalidCommand(string arg)
    {
    }

    public async Task<bool> ExecuteAsync()
    {
        return true;
    }
}
