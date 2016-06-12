using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsDoTo
{
    [Serializable]
    class Working : Event
    {
        public String Subject { get; set; }
        public String Details { get; set; }
        public String PredictedTime { get; set; }

        public Working()
        {
            Subject = String.Empty;
            Details = String.Empty;
            PredictedTime = String.Empty;
        }

        public Working(String aName, DateTime aDate, String aComment, Importance aEventImportance) : base(aName, aDate, aComment, aEventImportance)
        {
            Subject = String.Empty;
            Details = String.Empty;
            PredictedTime = String.Empty;
        }

        public override string Data()
        {
            return "Do:" + Subject + " Detailes: " + Details + " Predicted Time:" + PredictedTime;
        }
    }
}
