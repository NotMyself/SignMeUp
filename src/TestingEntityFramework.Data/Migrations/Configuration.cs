using System.Data.Entity.Migrations;

namespace TestingEntityFramework.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
