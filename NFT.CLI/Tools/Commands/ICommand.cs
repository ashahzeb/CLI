namespace NFT.CLI.Tools.Commands;

public interface ICommand
{
    Task<bool> ExecuteAsync();
}