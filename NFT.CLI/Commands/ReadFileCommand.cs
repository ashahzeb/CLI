using MediatR;
using NFT.CLI.Tools.Commands;

namespace NFT.CLI.Commands;

public class ReadFileCommand : BaseCommand
{
    public const string COMMAND_DEPLOY_NAME = "--read-file";
    
    private readonly IMediator _mediator;
    
    public ReadFileCommand(string[] args, IMediator mediator) : base(args)
    {
        _mediator = mediator;
    }

    protected override async Task<bool> PerformActionAsync()
    {
        var cmd = new NFT.Application.Commands.ReadFileCommand(OptionValue);
        var response = await _mediator.Send(cmd);
        Console.WriteLine(response.Message);
        return response.IsSuccess;
    }
}