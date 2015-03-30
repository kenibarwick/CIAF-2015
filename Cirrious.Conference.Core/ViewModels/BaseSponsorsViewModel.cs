using System.Collections.Generic;
using System.Linq;
using Cirrious.Conference.Core.Models.Raw;
using Cirrious.Conference.Core.ViewModels.Helpers;
using Cirrious.MvvmCross.Commands;

namespace Cirrious.Conference.Core.ViewModels
{
    public class BaseSponsorsViewModel
        : BaseConferenceViewModel
    {
        protected void LoadFrom(IEnumerable<Sponsor> source)
        {
            var sponsors = source
                .ToList();
            sponsors.Sort((a, b) => a.DisplayOrder.CompareTo(b.DisplayOrder));
            Sponsors = sponsors.Select(x => new WithCommand<Sponsor>(x, new MvxRelayCommand(() => ShowWebPage(x.Url)))).ToList();
        }

        public IList<WithCommand<Sponsor>> Sponsors { get; private set; }        
    }
}