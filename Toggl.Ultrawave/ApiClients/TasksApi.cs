using System;
using System.Collections.Generic;
using Toggl.Ultrawave.Models;
using Toggl.Ultrawave.Network;
using Toggl.Ultrawave.Serialization;

namespace Toggl.Ultrawave.ApiClients
{
    internal sealed class TasksApi : BaseApi, ITasksApi
    {
        private readonly TaskEndpoints endpoints;

        public TasksApi(TaskEndpoints endpoints, IApiClient apiClient, IJsonSerializer serializer, Credentials credentials)
            : base(apiClient, serializer, credentials)
        {
            this.endpoints = endpoints;
        }

        public IObservable<List<Task>> GetAll()
            => CreateObservable<List<Task>>(endpoints.Get, AuthHeader);
    }
}
