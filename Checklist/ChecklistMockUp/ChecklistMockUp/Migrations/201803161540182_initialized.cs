namespace ChecklistMockUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialized : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Widget");
            AlterColumn("dbo.Widget", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Widget", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Widget");
            AlterColumn("dbo.Widget", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Widget", "Id");
        }
    }
}
