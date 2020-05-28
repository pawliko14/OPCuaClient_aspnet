namespace SimpleWebOPC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OpcUaUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(10)]
        public string Login { get; set; }

        [StringLength(10)]
        public string Password { get; set; }

        [StringLength(10)]
        public string Privilages { get; set; }

        [StringLength(10)]
        public string TestRow { get; set; }

        
        public string LoginErrorMessage { get; set; }
    }
}
