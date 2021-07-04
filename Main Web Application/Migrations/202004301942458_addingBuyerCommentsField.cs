namespace Main_Web_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingBuyerCommentsField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectionObjects", "BuyerComments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CollectionObjects", "BuyerComments");
        }
    }
}
