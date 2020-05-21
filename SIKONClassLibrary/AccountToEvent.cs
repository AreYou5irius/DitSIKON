namespace SIKONClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountToEvent")]
    public partial class AccountToEvent
    {
        public int ID { get; set; }

        public int Event_ID { get; set; }

        [StringLength(50)]
        public string Account_ID { get; set; }

        public virtual Event Event { get; set; }


        public override string ToString()
        {
            return $"Event_ID: {Event_ID}";
        }
    }
}
