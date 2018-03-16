namespace ChecklistMockUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Widget",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WidgetXML = c.String(),
                        KeyName = c.String(),
                        KeyValue = c.Boolean(nullable: false),
                        UserEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Widget");
        }
    }
}
