namespace SIKONClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeToEvent")]
    public partial class TimeToEvent : IComparable<TimeToEvent>
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Event_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Time { get; set; }

        public virtual Event Event { get; set; }

        public override string ToString()
        {
            return $"{Time.DayOfWeek} {string.Format("{0:00}:{1:00}", Time.Hour, Time.Minute)}"; 
        }

        private sealed class TimeEqualityComparer : IEqualityComparer<TimeToEvent>
        {
            public bool Equals(TimeToEvent x, TimeToEvent y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Time.Equals(y.Time);
            }

            public int GetHashCode(TimeToEvent obj)
            {
                return obj.Time.GetHashCode();
            }
        }

        public static IEqualityComparer<TimeToEvent> TimeComparer { get; } = new TimeEqualityComparer();

        public int CompareTo(TimeToEvent other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var eventIdComparison = Event_ID.CompareTo(other.Event_ID);
            if (eventIdComparison != 0) return eventIdComparison;
            return Time.CompareTo(other.Time);
        }
    }
}
