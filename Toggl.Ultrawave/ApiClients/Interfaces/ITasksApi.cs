using System;
using System.Collections.Generic;
using Toggl.Ultrawave.Models;

namespace Toggl.Ultrawave.ApiClients
{
    public interface ITasksApi
    {
        IObservable<List<Task>> GetAll();
    }
}