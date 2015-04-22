using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.Platform.Tasks;

namespace Cirrious.Conference.Core.ViewModels
{
    public class MapViewModel
        : BaseViewModel
    {
        public string Name { get { return "Bentley Wildfowl & Motor Museum"; } }
        public string Address { get { return "Halland, East Sussex BN8 5AF"; } }
        public string LocationWebPage { get { return "http://www.chilledinafieldfestival.co.uk/the-location.html"; } }

        public string Phone { get { return "+44 (0)7795 666588"; } }
        public string Email { get { return "contact@chilledinafieldfestival.co.uk"; } }
        public double Latitude { get { return 50.923427; } }
        public double Longitude { get { return 0.112631; } }


        public IMvxCommand PhoneCommand
        {
            get
            {
                return new MvxRelayCommand(() => MakePhoneCall(Name, Phone));
            }
        }

        public IMvxCommand EmailCommand
        {
            get
            {
                return new MvxRelayCommand(() => ComposeEmail(Email, "About Chilled in a Field - sent from the app", "This years Chilled in a Field"));
            }
        }

        public IMvxCommand WebPageCommand
        {
            get
            {
                return new MvxRelayCommand(() => ShowWebPage(LocationWebPage));
            }
        }
    }
}