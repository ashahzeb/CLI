using MediatR;
using NFT.Application.Queries;
using NFT.CLI.Tools.Commands;

namespace NFT.CLI.Commands;

public class WalletOwnershipCommand : BaseCommand
{
    public const string COMMAND_DEPLOY_NAME = "--wallet";
    
    private readonly IMediator _mediator;
    
    public WalletOwnershipCommand(string[] args, IMediator mediator) : base(args)
    {
        _mediator = mediator;
    }

    protected async override Task<bool> PerformActionAsync()
    {
        var query = new GetWalletOwnershipQuery(OptionValue);
        var response = await _mediator.Send(query);
        var message = response.Message;
        if (response.Tokens.Any())
        {
            message += Environment.NewLine + string.Join(Environment.NewLine, response.Tokens); 
        }
       
        Console.WriteLine(message);
        return response.IsSuccess;
    }
}