using System;
using System.Data.Common;
using System.Data.Entity.Migrations;
using SignMeUp.Core.Extensions;
using SignMeUp.Data;
using SignMeUp.Data.Migrations;

namespace SignMeUp.Tests.Helpers
{
    public class TestDataContextFactory
    {
        private const string ConnectionString = "Data Source={0}.sdf";
        private const string ProviderName = "System.Data.SqlServerCe.4.0";
        

        public static DataContext Build()
        {
            var databaseName = DateTime.Now.Ticks.ToString();
            StandUpTestDatabase(databaseName);
            return CreateDataContext(databaseName);
        }

        private static DataContext CreateDataContext(string databaseName)
        {
            var connection = DbProviderFactories.GetFactory(ProviderName).CreateConnection();
            connection.ConnectionString = ConnectionString.FormatWith(databaseName);
            return new DataContext(connection);
        }

        private static void StandUpTestDatabase(string databaseName)
        {
            var config = new Configuration
                             {
                                 ConnectionString = ConnectionString.FormatWith(databaseName),
                                 ConnectionProviderName = ProviderName, 
                                 AutomaticMigrationsEnabled = true
                             };

            new DbMigrator(config).Update();
        }
    }
}
