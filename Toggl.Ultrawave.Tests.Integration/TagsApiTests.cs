﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using FluentAssertions;
using Toggl.Multivac.Models;
using Toggl.Ultrawave.Models;
using Toggl.Ultrawave.Tests.Integration.BaseTests;
using Toggl.Ultrawave.Tests.Integration.Helper;
using Xunit;


namespace Toggl.Ultrawave.Tests.Integration
{
    public class TagsApiTests
    {
        public class TheGetAllMethod : AuthenticatedGetEndpointBaseTests<List<ITag>>
        {
            protected override IObservable<List<ITag>> CallEndpointWith(ITogglApi togglApi)
                => togglApi.Tags.GetAll();

            private readonly string[] tags1 =
            {
                "tag1",
                "tag2",
                "tag3"
            };

            private readonly string[] tags2 =
            {
                "tag3",
                "tag4",
                "tag5"
            };

            [Fact, LogTestInfo]
            public async System.Threading.Tasks.Task ReturnsTagsForAllWorkspaces()
            {
                var(togglApi, user) = await SetupTestUser();
                var otherWorkspace = await WorkspaceHelper.CreateFor(user);
                var timeEntry1 = createTimeEntry(user.Id, user.DefaultWorkspaceId, tags1);
                var timeEntry2 = createTimeEntry(user.Id, otherWorkspace.Id, tags2);
                await togglApi.TimeEntries.Create(timeEntry1);
                await togglApi.TimeEntries.Create(timeEntry2);

                var returnedTags = await CallEndpointWith(togglApi);

                returnedTags.Should().HaveCount(tags1.Length + tags2.Length);
                assertTags(returnedTags, tags1, user.DefaultWorkspaceId);
                assertTags(returnedTags, tags2, otherWorkspace.Id);
            }

            private void assertTags(List<ITag> returnedTags, string[] expectedTags, long expectedWorkspaceId)
            {
                foreach (var expectedTag in expectedTags)
                {
                    returnedTags.Should().Contain(t => t.Name == expectedTag && t.WorkspaceId == expectedWorkspaceId);
                }
            }

            private TimeEntry createTimeEntry(long userId, long workspaceId, string[] tags) => new TimeEntry
            {
                UserId = userId,
                WorkspaceId = workspaceId,
                Start = new DateTimeOffset(DateTime.Now),
                CreatedWith = Configuration.UserAgent.ToString(),
                Tags = new List<string>(tags)
            };
        }
    }
}
