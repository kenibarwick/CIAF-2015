using Cirrious.Conference.Core.ViewModels.SessionLists;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;



namespace Cirrious.Conference.Core.ViewModels
{
    public class SessionsViewModel
        : BaseConferenceViewModel
    {
        public IMvxCommand ShowTopicsCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<TopicsViewModel>()); }
        }
        public IMvxCommand ShowSpeakersCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<SpeakersViewModel>()); }
        }
        public IMvxCommand ShowSessionsCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<SessionListViewModel>()); }
        }

        public IMvxCommand ShowDayCommand
        {
            get { return new MvxRelayCommand<string>((day) => RequestNavigate<SessionListViewModel>(new { dayOfMonth = int.Parse(day) })); }
        }

        public IMvxCommand ShowDay1Command
        {
            get { return MakeDayCommand(1); }
        }

        public IMvxCommand ShowDay2Command
        {
            get { return MakeDayCommand(2); }
        }

        public IMvxCommand ShowDay3Command
        {
            get { return MakeDayCommand(3); }
        }

        private IMvxCommand MakeDayCommand(int whichDayOfMonth)
        {
            return new MvxRelayCommand(() => RequestNavigate<SessionListViewModel>(new { dayOfMonth = whichDayOfMonth }));
        }
    }
}