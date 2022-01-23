using System.Collections.Generic;

namespace Tfl.Core.Application.Interfaces
{
    public interface IEventStore
    {
        IEnumerable<IEvent> Get<T>(string eventType);
    }
}
