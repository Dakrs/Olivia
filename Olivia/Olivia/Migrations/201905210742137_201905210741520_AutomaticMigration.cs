namespace Olivia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201905210741520_AutomaticMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipe", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Recipe", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipe", "Description", c => c.String());
            AlterColumn("dbo.Recipe", "Name", c => c.String());
        }
    }
}
