
using MediatR;
using System.Threading.Tasks;
using BusinessFile.Backend.Domain.Common;

namespace BusinessFile.Backend.Application.EventBus.Bus
{
    public interface IDomainBus
    {
     
        Task<TResponse> ExecuteAsync<T, TResponse>(T command) where T : IRequest<TResponse>;
        Task RaiseEvent<T>(T @event) where T : DomainEvent;
    }
}
