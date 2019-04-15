namespace SecuroteckWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usercontext1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        APIKey = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.APIKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
