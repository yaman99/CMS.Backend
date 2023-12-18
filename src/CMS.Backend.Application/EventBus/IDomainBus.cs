
using MediatR;
using System.Threading.Tasks;
using CMS.Backend.Domain.Common;

namespace CMS.Backend.Application.EventBus.Bus
{
    public interface IDomainBus
    {
     
        Task<TResponse> ExecuteAsync<T, TResponse>(T command) where T : IRequest<TResponse>;
        Task RaiseEvent<T>(T @event) where T : DomainEvent;
    }
}
