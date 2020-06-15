namespace SIKONClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Autist")]
    public partial class Autist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autist()
        {
            Account = new HashSet<Account>();
        }

        public int ID { get; set; }

        [StringLength(30)]
        public string ContactPerson { get; set; }

        public bool? Badge { get; set; }

        public bool? SpaceReservation { get; set; }

        public bool? BreakRoom { get; set; }

        public bool? SpecialFood { get; set; }

        public bool? EatingSpace { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Account { get; set; }
    }
}
