using System.Data.Common;
using System.Data.Entity;
using SignMeUp.Core;

namespace SignMeUp.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {}

        public DataContext(DbConnection connection): base(connection, true)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id)
                .ToTable("Users");

            base.OnModelCreating(modelBuilder);
        } 
    }
}
