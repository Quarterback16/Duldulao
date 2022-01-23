using System;

namespace Tfl.Core.Application.Interfaces
{
    public interface IEvent
    {
        Guid Id { get; set; }
        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
