namespace Vidly1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5d91a911-9bc5-40e1-881a-a3f8ba51f56d', N'admin@vidly1.com', 0, N'AEZ78A9P3idqRED5wWYaslZqGkogbqzt2I8SVrr8Sv0lfB6DvwJ/l0zC8M74dXeuTg==', N'f1e7a7ee-410b-4ba3-83c3-cbbf1376dc4a', NULL, 0, 0, NULL, 1, 0, N'admin@vidly1.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ced2907b-8578-4bbc-a331-6e0f736de827', N'gust@vidly1.com', 0, N'APsmHRz7KHjSbFxzAA4OR1Zg13/oJYHUD7aaSuN4FLNfNjZjtYKtwt71FAKUrNPC8Q==', N'b6159d22-ee65-49a2-9cd4-3e53b3783257', NULL, 0, 0, NULL, 1, 0, N'gust@vidly1.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c5927f0d-e0cc-4cd2-bb54-53041c1f9c6d', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5d91a911-9bc5-40e1-881a-a3f8ba51f56d', N'c5927f0d-e0cc-4cd2-bb54-53041c1f9c6d')

");
        }
        
        public override void Down()
        {
        }
    }
}
