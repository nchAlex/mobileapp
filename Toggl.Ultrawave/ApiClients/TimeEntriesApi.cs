using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Toggl.Multivac.Models;
using Toggl.Ultrawave.Models;
using Toggl.Ultrawave.Network;
using Toggl.Ultrawave.Serialization;

namespace Toggl.Ultrawave.ApiClients
{
    internal sealed class TimeEntriesApi : BaseApi, ITimeEntriesApi
    {
        private readonly TimeEntryEndpoints endPoints;

        public TimeEntriesApi(TimeEntryEndpoints endPoints, IApiClient apiClient, IJsonSerializer serializer, Credentials credentials)
            : base(apiClient, serializer, credentials)
        {
            this.endPoints = endPoints;
        }

        public IObservable<List<ITimeEntry>> GetAll()
        {
            var observable = CreateObservable<List<TimeEntry>>(endPoints.Get, AuthHeader);
            return observable.Select(timeEntries => timeEntries?.Cast<ITimeEntry>().ToList());
        }

        public IObservable<ITimeEntry> Create(ITimeEntry timeEntry)
        {
            var endPoint = endPoints.Post(timeEntry.WorkspaceId);
            var timeEntryCopy = timeEntry as TimeEntry ?? new TimeEntry(timeEntry);
            var observable = CreateObservable(endPoint, AuthHeader, timeEntryCopy, SerializationReason.Post);
            return observable;
        }
    }
}
