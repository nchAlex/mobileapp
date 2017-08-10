﻿using System;
using Realms;
using Toggl.PrimeRadiant.Models;
using System.Collections.Generic;

namespace Toggl.PrimeRadiant.Realm
{
    internal partial class RealmTimeEntry : RealmObject, IDatabaseTimeEntry
    {
        public bool Billable { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset? Stop { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public IList<string> Tags { get; set; }

        public IList<int> TagIds { get; set; }

        public DateTimeOffset At { get; set; }

        public DateTimeOffset? ServerDeletedAt { get; set; }

        public string CreatedWith { get; set; }

        public bool IsDeleted { get; set; }

        public RealmWorkspace RealmWorkspace { get; set; }

        public long WorkspaceId => RealmWorkspace?.Id ?? 0;

        public IDatabaseWorkspace Workspace => RealmWorkspace;

        public RealmProject RealmProject { get; set; }

        public long? ProjectId => RealmProject?.Id;

        public IDatabaseProject Project => RealmProject;

        public RealmTask RealmTask { get; set; }

        public long? TaskId => RealmTask?.Id;

        public IDatabaseTask Task => RealmTask;

        public RealmUser RealmUser { get; set; }

        public long UserId => RealmUser?.Id ?? 0;

        public IDatabaseUser User => RealmUser;
    }
}
