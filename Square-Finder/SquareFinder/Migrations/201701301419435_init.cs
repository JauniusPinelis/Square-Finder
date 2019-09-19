namespace SquareFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PointLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        PointList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PointLists", t => t.PointList_Id)
                .Index(t => t.PointList_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points", "PointList_Id", "dbo.PointLists");
            DropIndex("dbo.Points", new[] { "PointList_Id" });
            DropTable("dbo.Points");
            DropTable("dbo.PointLists");
        }
    }
}
