namespace WpfLaba1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Heroes", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Heroes", "Hp", c => c.Int(nullable: false));
            AlterColumn("dbo.Heroes", "Energy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Heroes", "Energy", c => c.String());
            AlterColumn("dbo.Heroes", "Hp", c => c.String());
            AlterColumn("dbo.Heroes", "Name", c => c.String());
        }
    }
}
