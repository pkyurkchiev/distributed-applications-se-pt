namespace MC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMovieColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Genres", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Movies", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "IsActive");
            DropColumn("dbo.Genres", "IsActive");
        }
    }
}
