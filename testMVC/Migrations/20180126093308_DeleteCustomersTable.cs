using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace testMVC.Migrations
{
    public partial class DeleteCustomersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Rentals");
            migrationBuilder.DropForeignKey("FK_Customers_Rooms_RoomId1", "Customers");
            migrationBuilder.DropIndex(name: "IX_Customers_RoomId1", table: "Customers");
            //migrationBuilder.DropColumn("RoomId", "Customers");
            migrationBuilder.DropColumn("RoomId1", "Customers");

            migrationBuilder.DropPrimaryKey("PK_Customers", "Customers");
            migrationBuilder.DropTable("Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
