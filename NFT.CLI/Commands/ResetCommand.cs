using MediatR;
using NFT.CLI.Tools.Commands;

namespace NFT.CLI.Commands;

public class ResetCommand : BaseCommand
{
    public const string COMMAND_DEPLOY_NAME = "--reset";
    
    private readonly IMediator _mediator;
    
    public ResetCommand(string[] args, IMediator mediator) : base(args)
    {
        _mediator = mediator;
    }

    protected async override Task<bool> PerformActionAsync()
    {
        var cmd = new NFT.Application.Commands.ResetCommand();
        var response = await _mediator.Send(cmd);
        Console.WriteLine(response);
        return true;
    }
}