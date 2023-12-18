using MediatR;
using System;

namespace CMS.Backend.Domain.Common
{
    //Base event as base notification
    public abstract class DomainEvent : INotification
    {
        protected DomainEvent()
        {
            Timestamp = DateTime.Now;
        }

        //Stamp the current fired event with its equivalent execution time
        public DateTime Timestamp { get; }
    }
}
