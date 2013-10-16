using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto.Domain.CommandHandlers;
using Proto.Domain.Services;

namespace Proto.Domain.CrossCuttingConcerns
{
    public class ValidationCommandHandlerDecorator<TCommand>
        : ICommandHandler<TCommand>
    {
        private readonly IValidator validator;
        private readonly ICommandHandler<TCommand> handler;

        public ValidationCommandHandlerDecorator(
            IValidator validator,
            ICommandHandler<TCommand> handler)
        {
            this.validator = validator;
            this.handler = handler;
        }

        void ICommandHandler<TCommand>.Handle(TCommand command)
        {
            // validate the supplied command (throws when invalid).
            this.validator.ValidateObject(command);

            // forward the (valid) command to the real
            // command handler.
            this.handler.Handle(command);
        }
    }
}
