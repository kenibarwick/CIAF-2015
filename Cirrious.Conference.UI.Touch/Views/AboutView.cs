using System.Drawing;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cirrious.Conference.UI.Touch
{
    public class AboutView : MvxBindingTouchViewController<AboutViewModel>
    {
        private int HeadingWidth = AppDelegate.IsPad ? 700 : 300;

        public AboutView(MvxShowViewModelRequest request) : base(request)
        {
           if (AppDelegate.IsPad) {
              this.View.Frame = new RectangleF (0, 0, 768, 1024);
           }
        }

        private UIScrollView _scrollview;
        private int _currentTop = 0;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Black;
			
			   NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Tweet", UIBarButtonItemStyle.Bordered, (sender, e) => ViewModel.ShareGeneralCommand.Execute()), false);

            _scrollview = AppDelegate.IsPad ? new UIScrollView(new RectangleF(0,0,768,1024)) : new UIScrollView(new RectangleF(0,0,320,365)) ;

            View.AddSubview(_scrollview);
			
            AddHeading("ConferenceApp");
            AddTextBlock("AboutConferenceApp");
            AddCommand(ViewModel.ContactSlodgeCommand, "StuartLinkText", "appbar.feature.email.rest");
            AddCommand(ViewModel.ContactGavinBryanCommand, "GavinLinkText", "appbar.feature.email.rest");
            
            AddHeading("Conference");
            AddTextBlock("AboutConference");
            AddCommand(ViewModel.ShowConferenceCommand,"ConferenceLinkText","appbar.link");

            AddHeading("ConferenceOrganisation");
            AddTextBlock("AboutConferenceOrganisation");
            AddCommand(ViewModel.ShowConferenceOrganisersCommand,"ConferenceOrganisationLinkText","appbar.link");
			AddTextBlock("Disclaimer");

            AddHeading("MvvmCross");
            AddTextBlock("AboutMvvmCross");
            AddCommand(ViewModel.ContactSlodgeCommand, "StuartLinkText", "appbar.feature.email.rest");
            AddTextBlock("ForMvvmSource");
            AddCommand(ViewModel.MvvmCrossOnGithubCommand, "MvvmCrossLinkText", "appbar.link");
            AddTextBlock("ForXamarin");
            AddCommand(ViewModel.MonoTouchCommand, "MonoTouch", "appbar.link");
            AddCommand(ViewModel.MonoDroidCommand, "MonoForAndroid", "appbar.link");
                            
            AddTextBlock("DisclaimerMono");

            _scrollview.ContentSize = new SizeF(320, _currentTop);
        }

        private string GetText(string which)
        {
            return ViewModel.TextSource.GetText(which);
        }

        private void AddHeading(string which)
        {
            _currentTop += 10;
            var frame = new RectangleF(10, _currentTop, HeadingWidth, 30);
            var view = new UILabel(frame);
            view.BackgroundColor = UIColor.Black;
            view.TextColor = UIColor.White;
            view.Text = GetText(which);
            view.Font = UIFont.FromName("Helvetica", 17);
            view.AdjustsFontSizeToFitWidth = false;
            AddView(view);
            _currentTop += 2;
        }

        private void AddTextBlock(string which)
        {
            var text = GetText(which);
            var nsText = new NSString(text);
            var font = UIFont.FromName("Helvetica", 13);
            var size = nsText.StringSize(font, new SizeF(HeadingWidth,100000), UILineBreakMode.WordWrap);
            var frame = new RectangleF(10, _currentTop, HeadingWidth, size.Height);
            var view = new UILabel(frame);
            view.BackgroundColor = UIColor.Black;
            view.AutoresizingMask = UIViewAutoresizing.None;
            view.AdjustsFontSizeToFitWidth = false;
            view.TextColor = UIColor.White;
            view.Font = font;
            view.LineBreakMode = UILineBreakMode.WordWrap;
            view.Text = GetText(which);
            view.Lines = 0;
            AddView(view);
        }

        private void AddCommand(IMvxCommand command, string whichText, string image)
        {
            var frame = new RectangleF(10, _currentTop, 280, 37);
            var button = UIButton.FromType(UIButtonType.Custom);
            button.Frame = frame;
            button.BackgroundColor = UIColor.Black;
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.SetTitle(GetText(whichText), UIControlState.Normal);
            button.SetImage(UIImage.FromFile("ConfResources/Images/" + image + ".png"), UIControlState.Normal);
            AddView(button);
			
            button.TouchDown += (sender, e) => command.Execute();
        }

        private void AddView(UIView view)
        {
            _scrollview.AddSubview(view);
            _currentTop += (int)view.Frame.Height;
        }
    }
}