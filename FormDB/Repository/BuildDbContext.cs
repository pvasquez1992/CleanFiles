using FormDB.Dto;
using System.Data.Entity;

namespace FormDB.Repository
{
    public class BuildDbContext : DbContext
    {

        public virtual DbSet<CustomerDto> Customer { get; set; }
        public BuildDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            base.OnModelCreating(modelBuilder);
        }

    }
}
