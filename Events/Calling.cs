using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsDoTo
{
    [Serializable]
    class Calling : Event
    {
        public String To { get; set; }
        public String About { get; set; }
        public String Details { get; set; }

        public Calling()
        {
            To = String.Empty;
            About = String.Empty;
            Details = String.Empty;
        }
        public Calling(string aName, DateTime aDate, string aComment, Importance aEventImportance) : base(aName, aDate, aComment, aEventImportance)
        {
            To = String.Empty;
            About = String.Empty;
            Details = String.Empty;
        }

        public override string Data()
        {
            return "Zadzwon do:" + To + " w sprawie: " + About + ". Pamietaj o:" + Details;
        }
    }
}
