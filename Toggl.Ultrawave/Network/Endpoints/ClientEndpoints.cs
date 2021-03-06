﻿using System;

namespace Toggl.Ultrawave.Network
{
    internal struct ClientEndpoints
    {
        private readonly Uri baseUrl;

        public ClientEndpoints(Uri baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public Endpoint Get => Endpoint.Get(baseUrl, "me/clients");

        public Endpoint Post(long workspaceId)
            => Endpoint.Post(baseUrl, $"workspaces/{ workspaceId }/clients");
    }
}
