using OffWork.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OffWork.DataAccess
{
    public class OffWorkContext : DbContext
    {
        public OffWorkContext() : base("OffWorkContext")
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<AbsenceType> AbsenceType { get; set; }
        public DbSet<AbsenceDetail> AbsenceDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}