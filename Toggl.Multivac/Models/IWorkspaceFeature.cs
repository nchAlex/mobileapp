using System;

namespace Toggl.Multivac.Models
{
    public interface IWorkspaceFeature : IBaseModel
    {
        int WorkspaceId { get; }

        WorkspaceFeatureId FeatureId { get; }

        bool Enabled { get; }
    }
}
