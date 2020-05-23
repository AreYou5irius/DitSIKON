

namespace SIKONClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            AccountToEvent = new HashSet<AccountToEvent>();
            Question = new HashSet<Question>();
            TimeToEvent = new HashSet<TimeToEvent>();
        }

        public TimeToEvent Time { get; set; }

        public int ID { get; set; }

        public int? Room_ID { get; set; }

        [StringLength(50)]
        public string Owner_ID { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(50)]
        public string Speaker { get; set; }

        [StringLength(3000)]
        public string Description { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountToEvent> AccountToEvent { get; set; }

        public virtual Room Room { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Question { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeToEvent> TimeToEvent { get; set; }

        protected bool Equals(Event other)
        {
            return Room_ID == other.Room_ID && Owner_ID == other.Owner_ID && Subject == other.Subject && Category == other.Category && Speaker == other.Speaker && Description == other.Description && Equals(Account, other.Account) && Equals(Room, other.Room);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Event) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Room_ID.GetHashCode();
                hashCode = (hashCode * 397) ^ (Owner_ID != null ? Owner_ID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Category != null ? Category.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Speaker != null ? Speaker.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Account != null ? Account.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Room != null ? Room.GetHashCode() : 0);
                return hashCode;
            }
        }

        
    }
}
