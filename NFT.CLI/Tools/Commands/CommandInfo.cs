using MediatR;

namespace NFT.CLI.Tools.Commands
{
    public interface ICommandInfo
    {
        string Name { get; }
        ICommand CreateCommand(string[] args);
    };

    public class CommandInfo<T> : ICommandInfo
        where T : class, ICommand
    {
        private readonly IMediator _mediator;
        public string Name { get; }
        public CommandInfo(string name, IMediator mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public ICommand CreateCommand(string[] args)
        {
            var typeClient = typeof(T);
            var constructor = typeClient.GetConstructor(new Type[] { typeof(string[]), typeof(IMediator) });
            if (constructor == null)
                throw new Exception($"Command Type {typeClient.FullName} is missing constructor");

            return constructor.Invoke(new object[] { args, _mediator }) as T;
        }
    }
}