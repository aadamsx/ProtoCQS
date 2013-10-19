namespace Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
