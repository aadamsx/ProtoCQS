using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Domain.QueryHandlers;
using SimpleInjector;

namespace Domain.CrossCuttingConcerns
{
    public class ValidationQueryHandlerDecorator<TQuery, TResult>
        : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IServiceProvider provider;
        private readonly IQueryHandler<TQuery, TResult> decorated;

        [DebuggerStepThrough]
        public ValidationQueryHandlerDecorator(
            Container container,
            IQueryHandler<TQuery, TResult> decorated)
        {
            // Got the following line (without the case from:
            // http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=92 )
            // but this might not be the same Container.  Look into this case.
            this.provider = container as IServiceProvider;  
            this.decorated = decorated;
        }

        [DebuggerStepThrough]
        public TResult Handle(TQuery query)
        {
            var validationContext =
                new ValidationContext(query, this.provider, null);

            Validator.ValidateObject(query, validationContext);

            return this.decorated.Handle(query);
        }
    }
}
