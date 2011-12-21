using System.Data.Entity.Migrations;

namespace SignMeUp.Data.Migrations
{
    public class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable("Users", b => new
            {
                Id = b.Int(nullable: false, identity: true),
                FirstName = b.String(nullable: false, maxLength: 255),
                LastName = b.String(nullable: false, maxLength: 255),
                Email = b.String(nullable: false, maxLength: 255),
            })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
                DropTable("Users");
        }
    }
}
