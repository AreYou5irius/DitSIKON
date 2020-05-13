namespace SIKONClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Event = new HashSet<Event>();
            Question = new HashSet<Question>();
        }

        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        public int? Autist_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(1)]
        public string AccountType { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        public virtual Autist Autist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Event { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Question { get; set; }
    }
}
