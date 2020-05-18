namespace SIKONrest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Account_ID { get; set; }

        public int? Event_ID { get; set; }

        [StringLength(30)]
        public string Subject { get; set; }

        [StringLength(3000)]
        public string Description { get; set; }

        public bool? Anonymity { get; set; }

        public virtual Account Account { get; set; }

        public virtual Event Event { get; set; }
    }
}
