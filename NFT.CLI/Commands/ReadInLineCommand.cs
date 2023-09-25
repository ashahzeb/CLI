using MediatR;
using NFT.CLI.Tools.Commands;

namespace NFT.CLI.Commands;

public class ReadInLineCommand : BaseCommand
{
    public const string COMMAND_DEPLOY_NAME = "--read-inline";

    private readonly IMediator _mediator;
    
    public ReadInLineCommand(string[] args, IMediator mediator) : base(args)
    {
        _mediator = mediator;
    }

    protected override async Task<bool> PerformActionAsync()
    {
        var cmd = new NFT.Application.Commands.ReadInLineCommand(OptionValue);
        var response = await _mediator.Send(cmd);
        Console.WriteLine(response.Message);
        return response.IsSuccess;
    }
}