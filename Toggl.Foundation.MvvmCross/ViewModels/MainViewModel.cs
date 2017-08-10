﻿using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Toggl.Foundation.MvvmCross.Parameters;
using Toggl.Multivac;

namespace Toggl.Foundation.MvvmCross.ViewModels
{
    public sealed class MainViewModel : MvxViewModel
    {
        private readonly ITimeService timeService;
        private readonly IMvxNavigationService navigationService;

        public IMvxAsyncCommand StartTimeEntryCommand { get; }

        public IMvxAsyncCommand OpenSettingsCommand { get; }

        public MainViewModel(ITimeService timeService, IMvxNavigationService navigationService)
        {
            Ensure.Argument.IsNotNull(timeService, nameof(timeService));
            Ensure.Argument.IsNotNull(navigationService, nameof(navigationService));

            this.timeService = timeService;
            this.navigationService = navigationService;

            OpenSettingsCommand = new MvxAsyncCommand(openSettings);
            StartTimeEntryCommand = new MvxAsyncCommand(startTimeEntry);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            navigationService.Navigate<SuggestionsViewModel>();
            navigationService.Navigate<TimeEntriesLogViewModel>();
        }

        private Task openSettings()
            => navigationService.Navigate<SettingsViewModel>();

        private Task startTimeEntry() =>
            navigationService.Navigate<StartTimeEntryViewModel, DateParameter>(
                DateParameter.WithDate(timeService.CurrentDateTime)
            );
    }
}
