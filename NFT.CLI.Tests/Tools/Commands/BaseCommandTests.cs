namespace NFT.CLI.Tests.Tools.Commands;

using System;
using System.Threading.Tasks;
using NFT.CLI.Tools.Commands;
using Xunit;

public class BaseCommandTests
{
    [Fact]
    public async Task ExecuteAsync_SuccessfulAction_ReturnsTrue()
    {
        // Arrange
        var args = new string[] { "optionValue" };
        var command = new SampleCommand(args);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExecuteAsync_ExceptionThrown_ReturnsFalse()
    {
        // Arrange
        var args = new string[] { "optionValue" };
        var command = new ExceptionCommand(args);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExecuteAsync_NoArgs_ReturnsTrue()
    {
        // Arrange
        var args = new string[0];
        var command = new SampleCommand(args);

        // Act
        var result = await command.ExecuteAsync();

        // Assert
        Assert.True(result);
    }

    private class SampleCommand : BaseCommand
    {
        public SampleCommand(string[] args) : base(args)
        {
        }

        protected override async Task<bool> PerformActionAsync()
        {
            // Simulate a successful action
            return await Task.FromResult(true);
        }
    }

    private class ExceptionCommand : BaseCommand
    {
        public ExceptionCommand(string[] args) : base(args)
        {
        }

        protected override async Task<bool> PerformActionAsync()
        {
            // Simulate an exception being thrown
            throw new Exception("Simulated exception");
        }
    }
}
