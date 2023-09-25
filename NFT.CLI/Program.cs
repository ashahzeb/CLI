using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NFT.Application;
using NFT.Application.Commands.Handlers;
using NFT.CLI.Commands;
using NFT.CLI.Tools.Commands;

namespace NFT.CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            
            var application = new Tools.Application(new List<ICommandInfo>()
            {
                new CommandInfo<ReadInLineCommand>(ReadInLineCommand.COMMAND_DEPLOY_NAME, mediator),
                new CommandInfo<ReadFileCommand>(ReadFileCommand.COMMAND_DEPLOY_NAME, mediator),
                new CommandInfo<ResetCommand>(ResetCommand.COMMAND_DEPLOY_NAME, mediator),
                new CommandInfo<NFTOwnershipCommand>(NFTOwnershipCommand.COMMAND_DEPLOY_NAME, mediator),
                new CommandInfo<WalletOwnershipCommand>(WalletOwnershipCommand.COMMAND_DEPLOY_NAME, mediator)
            });
            
            var exitCode = application.Execute(args);
            if (exitCode != 0)
            {
                Environment.Exit(-1);
            }
        }

        static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(ReadInLineCommandHandler).GetTypeInfo().Assembly));
            services.AddApplication();
            return services;
        }
    }
}