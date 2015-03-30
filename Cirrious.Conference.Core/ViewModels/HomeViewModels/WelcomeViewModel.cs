using Cirrious.Conference.Core.ViewModels.SessionLists;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;

namespace Cirrious.Conference.Core.ViewModels.HomeViewModels
{
    public class WelcomeViewModel
        : BaseConferenceViewModel
    {
        public IMvxCommand ShowSponsorsCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<SponsorsViewModel>()); }
        }
        public IMvxCommand ShowTeamCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<TeamViewModel>()); }
        }
        public IMvxCommand ShowMapCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<MapViewModel>()); }
        }
        public IMvxCommand ShowAboutCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<AboutViewModel>()); }
        }

        public IMvxCommand ShowSeasonsCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<HomeViewModel>()); }
        }

        public IMvxCommand ShowTopicsCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<TopicsViewModel>()); }
        }

        public IMvxCommand ShowSpeakersCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<SpeakersViewModel>()); }
        }

        public IMvxCommand ShowExhibitionCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<ExhibitionViewModel>()); }
        }

        public IMvxCommand ShowSiteMapViewCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<SiteMapViewModel>()); }
        }

        public IMvxCommand ShowSiteMapCommand
        {
            get { return new MvxRelayCommand(() => ShowWebPage("http://www.chilledinafieldfestival.co.uk/site-map.html")); }
        }

        public IMvxCommand ShowVenuesCommand
        {
            get { return new MvxRelayCommand(() => ShowWebPage("http://www.chilledinafieldfestival.co.uk/site-map.html")); }
        }

    }
}