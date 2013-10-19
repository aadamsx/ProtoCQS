﻿using Domain.CommandHandlers;
using Domain.Services;

namespace Domain.CrossCuttingConcerns
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