using System;
using Cirrious.Conference.UI.Touch.Interfaces;
using Cirrious.Conference.UI.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MonoTouch.UIKit;

namespace Cirrious.Conference.UI.Touch
{
   public class ConferenceTabletPresenter
       : MvxModalSupportTouchViewPresenter
       , ITabBarPresenterHost
   {
      public ConferenceTabletPresenter (UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
      {
      }

      protected override UINavigationController CreateNavigationController (UIViewController viewController)
      {
         var toReturn = base.CreateNavigationController (viewController);
         toReturn.NavigationBarHidden = true;
         return toReturn;
      }

      public ITabBarPresenter TabBarPresenter { get; set; }

      public override void Show (MvvmCross.Touch.Interfaces.IMvxTouchView view)
      {
         if (TabBarPresenter != null && view != TabBarPresenter) {
            TabBarPresenter.ShowView (view);
            return;
         }

         base.Show (view);
      }
   }
}

