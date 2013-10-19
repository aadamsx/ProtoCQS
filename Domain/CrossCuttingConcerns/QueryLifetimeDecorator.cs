using System;
using Domain.QueryHandlers;
using SimpleInjector;

namespace Domain.CrossCuttingConcerns
{
    public sealed class QueryLifetimeDecorator<TQuery, TResult>
            : IQueryHandler<TQuery, TResult>
            where TQuery : IQuery<TResult>
    {
        private readonly Container container;
        private Func<IQueryHandler<TQuery, TResult>> handlerCreator;

        public QueryLifetimeDecorator(
            Container container,
            Func<IQueryHandler<TQuery, TResult>> handlerCreator)
        {
            this.container = container;
            this.handlerCreator = handlerCreator;
        }

        public TResult Handle(TQuery query)
        {
            using (this.container.BeginLifetimeScope())
            {
                return this.handlerCreator().Handle(query);
            }
        }
    }
}
