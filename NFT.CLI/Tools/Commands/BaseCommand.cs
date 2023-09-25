namespace NFT.CLI.Tools.Commands;

public abstract class BaseCommand : ICommand
{
    public string OptionValue { get; set; }

    public BaseCommand(string[] args)
    {
        args = args ?? new string[0];
        if (args.Length > 0)
        {
            OptionValue = args[0];
        }
    }

    public async Task<bool> ExecuteAsync()
    {
        try
        {
            var success = await PerformActionAsync();
            if (!success)
                return false;

        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
    
    protected abstract Task<bool> PerformActionAsync();
}