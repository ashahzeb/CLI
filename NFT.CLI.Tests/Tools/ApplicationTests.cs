namespace NFT.CLI.Tests.Tools;

using System.Collections.Generic;
using Moq;
using NFT.CLI.Tools;
using NFT.CLI.Tools.Commands;
using Xunit;

public class ApplicationTests
{
    [Fact]
    public void Execute_UnknownCommand_ReturnsMinusOne()
    {
        // Arrange
        var commands = new List<ICommandInfo>
        {
            new Mock<ICommandInfo>().Object
        };
        var application = new Application(commands);
        var args = new string[] { "unknownCommand" };
        
        // Act
        var result = application.Execute(args);
        
        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Execute_KnownCommand_Failure_ReturnsMinusOne()
    {
        // Arrange
        var commandInfoMock = new Mock<ICommandInfo>();
        commandInfoMock.Setup(info => info.Name).Returns("knownCommand");
        commandInfoMock.Setup(info => info.CreateCommand(It.IsAny<string[]>())).Returns(() =>
        {
            var commandMock = new Mock<ICommand>();
            commandMock.Setup(command => command.ExecuteAsync()).ReturnsAsync(false);
            return commandMock.Object;
        });

        var commands = new List<ICommandInfo>
        {
            commandInfoMock.Object
        };
        
        var application = new Application(commands);
        var args = new string[] { "knownCommand" };

        // Act
        var result = application.Execute(args);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Execute_KnownCommand_Success_ReturnsZero()
    {
        // Arrange
        var commandInfoMock = new Mock<ICommandInfo>();
        commandInfoMock.Setup(info => info.Name).Returns("knownCommand");
        commandInfoMock.Setup(info => info.CreateCommand(It.IsAny<string[]>())).Returns(() =>
        {
            var commandMock = new Mock<ICommand>();
            commandMock.Setup(command => command.ExecuteAsync()).ReturnsAsync(true);
            return commandMock.Object;
        });

        var commands = new List<ICommandInfo>
        {
            commandInfoMock.Object
        };
        
        var application = new Application(commands);
        var args = new string[] { "knownCommand" };

        // Act
        var result = application.Execute(args);

        // Assert
        Assert.Equal(0, result);
    }
}
