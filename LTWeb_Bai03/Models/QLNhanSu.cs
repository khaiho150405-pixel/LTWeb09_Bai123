using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LTWeb_Bai03.Models
{
    public partial class QLNhanSu : DbContext
    {
        public QLNhanSu()
            : base("name=QLNhanSu")
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Img)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
