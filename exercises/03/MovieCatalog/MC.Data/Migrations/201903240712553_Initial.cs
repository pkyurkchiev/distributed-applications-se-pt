namespace MC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 300),
                        ReleaseDate = c.DateTime(),
                        ReleaseCountry = c.String(),
                        GenreId = c.Int(),
                        DirectorId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.DirectorId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GenreId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropIndex("dbo.Movies", new[] { "GenreId" });
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.Directors");
        }
    }
}
