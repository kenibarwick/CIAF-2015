using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using Cirrious.Conference.Core.ViewModels;

namespace Cirrious.Conference.UI.Droid.Views
{
    [Activity(Label = "DDD SouthWest")]
    public class HomeView : BaseTabbedView<HomeViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_Home);

            TabHost.TabSpec spec;     // Resusable TabSpec for each tab
            Intent intent;            // Reusable Intent for each tab

            // Initialize a TabSpec for each tab and add it to the TabHost
            spec = TabHost.NewTabSpec("welcome");
            spec.SetIndicator(this.GetText("Welcome"), Resources.GetDrawable(Resource.Drawable.Tab_Welcome));
            spec.SetContent(CreateIntentFor(ViewModel.Welcome));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("sessions");
            spec.SetIndicator(this.GetText("Sessions"), Resources.GetDrawable(Resource.Drawable.Tab_Sessions));
            spec.SetContent(CreateIntentFor(ViewModel.Sessions));
            TabHost.AddTab(spec);
            
            spec = TabHost.NewTabSpec("favorites");
            spec.SetIndicator(this.GetText("Favorites"), Resources.GetDrawable(Resource.Drawable.Tab_Favorites));
            spec.SetContent(CreateIntentFor(ViewModel.Favorites));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("tweets");
            spec.SetIndicator(this.GetText("Tweets"), Resources.GetDrawable(Resource.Drawable.Tab_Tweets));
            spec.SetContent(CreateIntentFor(ViewModel.Twitter));
            TabHost.AddTab(spec);

            //TabHost.TabChanged += (s, e) => UpdateTabColors();
            //UpdateTabColors();
        }

        private readonly Color _selectedColor = Color.ParseColor("#FF6A00");
        private readonly Color _nonSelectedColor = Color.Gray;

        private void UpdateTabColors()
        {
            var selected = TabHost.CurrentTab;
            for (var i = 0; i < TabHost.ChildCount; i++)
            {
                TabHost.TabWidget.GetChildAt(i).SetBackgroundColor(i == selected ? _selectedColor : _nonSelectedColor);
            }
        }
    }
}