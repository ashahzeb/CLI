using MediatR;
using NFT.Application.Queries;
using NFT.CLI.Tools.Commands;

namespace NFT.CLI.Commands;

public class NFTOwnershipCommand : BaseCommand
{
    public const string COMMAND_DEPLOY_NAME = "--nft";
    
    private readonly IMediator _mediator;
    
    public NFTOwnershipCommand(string[] args, IMediator mediator) : base(args)
    {
        _mediator = mediator;
    }

    protected async override Task<bool> PerformActionAsync()
    {
        var query = new GetNFTOwnerQuery(OptionValue);
        var response = await _mediator.Send(query);
        Console.WriteLine(response.Message);
        return response.IsSuccess;
    }
}