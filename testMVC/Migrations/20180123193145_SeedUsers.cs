using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace testMVC.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'1bb18f23-211e-45e9-899c-11b6e8479448', 0, N'1216373d-e05d-4973-9d6b-d66718d85d42', N'guest@alihotel.com', 0, 1, NULL, N'GUEST@ALIHOTEL.COM', N'GUEST@ALIHOTEL.COM', N'AQAAAAEAACcQAAAAEN1dpEkfT4kupI3I20KUYkOSIZa4LMgwkHUWNFp8+eYXekY7p8nzrG43IbuVtQSezg==', NULL, 0, N'bf2faa27-5fb4-41d9-b598-4e22b1195ed8', 0, N'guest@alihotel.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'ebd0f33e-cd80-4ed0-b5b5-11665b6bcddb', 0, N'723dccf9-0e30-46a6-8149-4ec1e84f3dc8', N'admin@alihotel.com', 0, 1, NULL, N'ADMIN@ALIHOTEL.COM', N'ADMIN@ALIHOTEL.COM', N'AQAAAAEAACcQAAAAEPokVPn/r25zNRFQA1HuDnCoZPWeL4V4TuY4GHI2bBb+Zr3EzeWA0e5EtF6SJnYEnA==', NULL, 0, N'a1df8d7d-0342-4c6f-afea-4f479b130ba0', 0, N'admin@alihotel.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'cb0736d1-e157-4a1e-9f29-cce043efa2ba', N'8a94fa5f-3722-449b-8b56-7da0e44ceb29', N'admin', N'ADMIN')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ebd0f33e-cd80-4ed0-b5b5-11665b6bcddb', N'cb0736d1-e157-4a1e-9f29-cce043efa2ba')

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
