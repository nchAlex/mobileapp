﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Toggl.Multivac.Models;
using Toggl.Ultrawave.Models;
using Toggl.Ultrawave.Network;
using Toggl.Ultrawave.Serialization;

namespace Toggl.Ultrawave.ApiClients
{
    internal sealed class ProjectsApi : BaseApi, IProjectsApi
    {
        private readonly ProjectEndpoints endPoints;

        public ProjectsApi(ProjectEndpoints endPoints, IApiClient apiClient, IJsonSerializer serializer, Credentials credentials)
            : base(apiClient, serializer, credentials)
        {
            this.endPoints = endPoints;
        }

        public IObservable<List<IProject>> GetAll()
        {
            var observable = CreateObservable<List<Project>>(endPoints.Get, AuthHeader);
            return observable.Cast<List<IProject>>();
        }

        public IObservable<IProject> Create(IProject project)
        {
            var endPoint = endPoints.Post(project.WorkspaceId);
            var projectCopy = project as Project ?? new Project(project);
            var observable = CreateObservable<Project>(endPoint, AuthHeader, projectCopy, SerializationReason.Post);
            return observable;
        }
    }
}
