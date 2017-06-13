using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Ultrawave.ApiClients
{
    public interface ITasksApi
    {
        IObservable<List<Task>> GetAll();
    }
}