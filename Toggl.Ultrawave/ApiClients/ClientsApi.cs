﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Toggl.Multivac.Models;
using Toggl.Ultrawave.Models;
using Toggl.Ultrawave.Network;
using Toggl.Ultrawave.Serialization;

namespace Toggl.Ultrawave.ApiClients
{
    internal sealed class ClientsApi : BaseApi, IClientsApi
    {
        private readonly ClientEndpoints endPoints;

        public ClientsApi(ClientEndpoints endPoints, IApiClient apiClient, IJsonSerializer serializer, Credentials credentials)
            : base(apiClient, serializer, credentials)
        {
            this.endPoints = endPoints;
        }

        public IObservable<List<IClient>> GetAll()
            => CreateListObservable<Client, IClient>(endPoints.Get, AuthHeader);

        public IObservable<IClient> Create(IClient client)
        {
            var endPoint = endPoints.Post(client.WorkspaceId);
            var clientCopy = client as Client ?? new Client(client);
            var observable = CreateObservable<Client>(endPoint, AuthHeader, clientCopy, SerializationReason.Post);
            return observable;
        }
    }
}
