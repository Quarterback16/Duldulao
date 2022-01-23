using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using Tfl.Core.Application.Interfaces;
using Tfl.Core.Data.Events;

namespace Tfl.Core.Data.Services
{
    public class ScheduleEventStore : IEventStore
    {
        public List<ScheduleEvent>? Events { get; set; }

        public ScheduleEventStore()
        {
        }

        public ScheduleEventStore(
            string jsonFile)
        {
            if (string.IsNullOrEmpty(jsonFile))
                jsonFile = "c:\\apps\\Schedules\\nfl-schedule-2021.json";
            using var r = new StreamReader(jsonFile);
            if (r == null)
            {
                Events = new List<ScheduleEvent>();
                return;
            }

            var json = r.ReadToEnd();
            if (json == null)
                Events = new List<ScheduleEvent>();
            else
                Events = JsonConvert.DeserializeObject<List<ScheduleEvent>>(
                    json);
        }

        //  Get all events for a specific aggregate (order by version)
        public IEnumerable<IEvent> Get<T>(
            Guid aggregateId, 
            int fromVersion)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> Get<T>(
            string eventType)
        {
            if (Events != null)
                return Events;
            return new List<IEvent>();
        }
    }
}
