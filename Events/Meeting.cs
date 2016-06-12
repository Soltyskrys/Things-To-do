using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsDoTo
{
    [Serializable]
    class Meeting : Event
    {
        public String With { get; set; }
        public String About { get; set; }
        public String Details { get; set; }
        public String PredictedTime { get; set; }

        public Meeting()
        {
            With = String.Empty;
            About = String.Empty;
            Details = String.Empty;
            PredictedTime = String.Empty;
        }

        public Meeting(string aName, DateTime aDate, string aComment, Importance aEventImportance) : base(aName, aDate, aComment, aEventImportance)
        {
        }

        public override string Data()
        {
            return "Hello";
        }
    }
}
