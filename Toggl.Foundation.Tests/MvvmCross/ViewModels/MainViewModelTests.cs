﻿﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Toggl.Foundation.MvvmCross.Parameters;
using Toggl.Foundation.MvvmCross.ViewModels;
using Toggl.Foundation.Tests.Generators;
using Xunit;

namespace Toggl.Foundation.Tests.MvvmCross.ViewModels
{
    public class MainViewModelTests
    {
        public class MainViewModelTest : BaseViewModelTests<MainViewModel>
        {
            protected override MainViewModel CreateViewModel()
                => new MainViewModel(TimeService, NavigationService);
        }

        public class TheConstructor : MainViewModelTest
        {
            [Theory]
            [ClassData(typeof(TwoParameterConstructorTestData))]
            public void ThrowsIfAnyOfTheArgumentsIsNull(bool useTimeService, bool useNavigationService)
            {
                var timeService = useTimeService ? TimeService : null;
                var navigationService = useNavigationService ? NavigationService : null;

                Action tryingToConstructWithEmptyParameters =
                    () => new MainViewModel(timeService, navigationService);

                tryingToConstructWithEmptyParameters
                    .ShouldThrow<ArgumentNullException>();
            }
        }

        public class TheViewAppearedMethod : MainViewModelTest
        {
            [Fact]
            public void RequestsTheSuggestionsViewModel()
            {
                ViewModel.ViewAppeared();

                NavigationService.Received().Navigate<SuggestionsViewModel>();
            }

            [Fact]
            public void RequestsTheLogTimeEntriesViewModel()
            {
                ViewModel.ViewAppeared();

                NavigationService.Received().Navigate<TimeEntriesLogViewModel>();
            }
        }

        public class TheStartTimeEntryCommand : MainViewModelTest
        {
            [Fact]
            public async Task NavigatesToTheStartTimeEntryViewModel()
            {
                await ViewModel.StartTimeEntryCommand.ExecuteAsync();

                await NavigationService.Received().Navigate<StartTimeEntryViewModel, DateParameter>(Arg.Any<DateParameter>());
            }

            [Fact]
            public async Task PassesTheCurrentDateToTheStartTimeEntryViewModel()
            {
                var date = DateTimeOffset.Now;
                TimeService.CurrentDateTime.Returns(date);

                await ViewModel.StartTimeEntryCommand.ExecuteAsync();

                await NavigationService.Received().Navigate<StartTimeEntryViewModel, DateParameter>(
                    Arg.Is<DateParameter>(parameter => parameter.DateString == date.ToString())
                );
            }
        }

        public class TheOpenSettingsCommand : MainViewModelTest
        {
            [Fact]
            public async Task NavigatesToTheSettingsViewModel()
            {
                await ViewModel.OpenSettingsCommand.ExecuteAsync();

                await NavigationService.Received().Navigate<SettingsViewModel>();
            }
        }
    }
}
