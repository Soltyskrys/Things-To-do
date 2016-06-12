using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsDoTo
{
    [Serializable]
    abstract public class Event : IComparable<Event>
    {
        public static List<Type> EventsType;
        static Event()
        {
            EventsType = new List<Type>();
            EventsType.Add(typeof(Meeting));
            EventsType.Add(typeof(Calling));
            EventsType.Add(typeof(Working));
        }


        public String Name { get; set; }
        public DateTime Date { get; set; }
        public String Comment { get; set; }
        public Importance EventImportance { get; set; }

        public String Description {
            get
            {
                return Data();
            }
        }

        public Event()
        {
            Name = String.Empty;
            Date = DateTime.Now;
            Comment = String.Empty;
            EventImportance = Importance.STANDART;
        }
        public Event(string aName,DateTime aDate,String aComment,Importance aEventImportance)
        {
            Name = aName;
            Date = aDate;
            Comment = aComment;
            EventImportance = aEventImportance;
        }

        abstract public String Data();

        public int CompareTo(Event other)
        {
            return Date.CompareTo(other.Date);
        }
    }
}
