
using BusinessFile.Backend.Domain.Common;
using BusinessFile.Backend.Application.EventBus.Bus;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BusinessFile.Backend.Application.EventBus
{
    public class MediatrBus : IDomainBus
    {
        private readonly IMediator _mediator;

        public MediatrBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<TResponse> ExecuteAsync<T, TResponse>(T command) where T : IRequest<TResponse>
        {
           return  await _mediator.Send(command);
        }

        public async Task RaiseEvent<T>(T @event) where T : DomainEvent
        {
            await _mediator.Publish(@event);
        }

    }
}
