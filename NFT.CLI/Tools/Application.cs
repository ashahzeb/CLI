using NFT.CLI.Tools.Commands;

namespace NFT.CLI.Tools;

public class Application
{
    IList<ICommandInfo> CommandInfos { get; }
    
    public Application(IList<ICommandInfo> commands)
    {
        CommandInfos = commands;
    }

    public int Execute(string[] args)
    {
        try
        {
            var commandInfo = FindCommandInfo(args[0]);
        
            if (commandInfo != null)
            {
                var command = commandInfo.CreateCommand(args.Skip(1).ToArray());
                var success = command.ExecuteAsync().Result;
                if (!success)
                {
                    return -1;
                }
            }
            else
            {
                Console.Error.WriteLine($"Unknown command: {args[0]}\n");
                PrintUsage();
                return -1;
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unknown error:");
            Console.Error.WriteLine(ex);
            return -1;
        }
    }

    private void PrintUsage()
    {
        Console.WriteLine(
            "Usage: program [--read-inline <json>] [--read-file <file>] [--nft <id>] [--wallet <address>] [--reset]");
    }

    private ICommandInfo FindCommandInfo(string name)
    {
        foreach (var commandInfo in CommandInfos)
        {
            if (commandInfo == null)
                continue;

            if (string.Equals(commandInfo.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return commandInfo;
            }
        }

        return null;
    }
}