namespace MC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDirector : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 300),
                        LastName = c.String(maxLength: 300),
                        UserName = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "DirectorId", c => c.Int());
            CreateIndex("dbo.Movies", "DirectorId");
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropColumn("dbo.Movies", "DirectorId");
            DropTable("dbo.Directors");
        }
    }
}
