using Cirrious.MvvmCross.Platform.Json;
using Newtonsoft.Json;

namespace Cirrious.Conference.Core.Models.Raw
{
    public class Sponsor
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public int DisplayOrder { get; set; }
    }
}