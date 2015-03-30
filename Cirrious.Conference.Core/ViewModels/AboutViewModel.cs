using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;

namespace Cirrious.Conference.Core.ViewModels
{
    public class AboutViewModel
        : BaseConferenceViewModel
    {
        public IMvxCommand ContactSlodgeCommand
        {
            get
            {
                return
                    new MvxRelayCommand(
                        () =>
                        ComposeEmail("me@slodge.com", "About MvvmCross and the DDD South West 4 app", "I've got a question"));
            }
        }

        public IMvxCommand ContactGavinBryanCommand
        {
          get
          {
            return
                new MvxRelayCommand(
                    () =>
                    ComposeEmail("gavinbryan@hotmail.com", "About the DDD South West 4 app", "I've got a question"));
          }
        }

        public IMvxCommand MvvmCrossOnGithubCommand
        {
            get
            {
                return
                    new MvxRelayCommand(
                        () =>
                        ShowWebPage("http://github.com/slodge/mvvmcross"));
            }
        }

        public IMvxCommand ShowConferenceCommand
        {
            get
            {
                return
                    new MvxRelayCommand(
                        () =>
                        ShowWebPage("http://www.chilledinafieldfestival.co.uk/"));
            }
        }

        public IMvxCommand ShowConferenceOrganisersCommand
        {
            get
            {
                return
                    new MvxRelayCommand(
                        () =>
                        ShowWebPage("http://www.developerdeveloperdeveloper.com"));
            }
        }

        public IMvxCommand MonoTouchCommand
        {
            get
            {
                return
                    new MvxRelayCommand(
                        () =>
                        ShowWebPage("http://ios.xamarin.com"));
            }
        }

        public IMvxCommand MonoDroidCommand
        {
            get
            {
                return
                    new MvxRelayCommand(
                        () =>
                        ShowWebPage("http://android.xamarin.com"));
            }
        }
    }
}