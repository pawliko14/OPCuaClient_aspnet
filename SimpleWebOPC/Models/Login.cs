namespace SimpleWebOPC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoginDatabeEntity : DbContext
    {
        public LoginDatabeEntity()
            : base("name=LoginDatabeEntity")
        {
        }

        public virtual DbSet<OpcUaUser> OpcUaUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new Exception();
        }
    }
}
