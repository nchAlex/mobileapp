using System;
using System.Collections.Generic;
using Toggl.Multivac;
using Toggl.Ultrawave.Models;

namespace Toggl.Ultrawave.ApiClients
{
    public interface IWorkspaceFeaturesApi
    {
        IObservable<List<WorkspaceFeature>> GetAll();
        IObservable<List<WorkspaceFeature>> GetEnabledFeatures();
        IObservable<List<WorkspaceFeature>> GetEnabledFeaturesForWorkspace(int workspaceId);
        IObservable<bool> IsFeatureEnabled(int workspaceId, WorkspaceFeatureId featureId);
    }
}
