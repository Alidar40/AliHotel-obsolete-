using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace testMVC.Migrations
{
    public partial class AddActiveRentersToRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Rooms_RoomId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RoomId",
                table: "Customers");
            */
            migrationBuilder.AddColumn<byte>(
                name: "ActiveRenters",
                table: "Rooms",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
            /*
            migrationBuilder.AlterColumn<byte>(
                name: "RoomId",
                table: "Customers",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RoomId1",
                table: "Customers",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Rooms_RoomId1",
                table: "Customers",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
                */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Rooms_RoomId1",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RoomId1",
                table: "Customers");
               */ 

            migrationBuilder.DropColumn(
                name: "ActiveRenters",
                table: "Rooms");
            /*
            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RoomId",
                table: "Customers",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Rooms_RoomId",
                table: "Customers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
                */
        }
    }
}
