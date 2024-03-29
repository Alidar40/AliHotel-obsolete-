﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace testMVC.Migrations
{
    public partial class CreateDatatables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    //Id = Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    RoomTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    RoomTypeId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomType_RoomTypeId1",
                        column: x => x.RoomTypeId1,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoomId = table.Column<byte>(type: "tinyint", nullable: false),
                    RoomId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Rooms_RoomId1",
                        column: x => x.RoomId1,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            
            migrationBuilder.CreateIndex(
                name: "IX_Customers_RoomId1",
                table: "Customers",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId1",
                table: "Rooms",
                column: "RoomTypeId1");
             

            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Rooms_RoomId1",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RoomId1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Customers");

            //Solving bug that creates error: 
            //"To change the IDENTITY property of a column, the column needs to be dropped and recreated."
            //Code generated by EF:
            /*migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            */
            /*
            migrationBuilder.AddColumn<Guid>(
                name: "Id2",
                table: "Rooms",
                nullable: true);

            migrationBuilder.Sql("update rooms set Id2 = Id");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "Id2",
                table: "Rooms",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Rooms",
                nullable: false,
                oldNullable: true);
            //------



            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(byte));

            //Solving bug that creates error: 
            //"To change the IDENTITY property of a column, the column needs to be dropped and recreated."
            //Code generated by EF:
            /*migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            */
            /*
            migrationBuilder.AddColumn<Guid>(
                name: "Id2",
                table: "Customers",
                nullable: true);

            migrationBuilder.Sql("update customers set Id2 = Id");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Id2",
                table: "Rooms",
                newName: "Customers");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Customers",
                nullable: false,
                oldNullable: true);

            //------

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomType");

            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Rooms_RoomId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RoomId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Rooms",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte>(
                name: "RoomId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Customers",
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
    }
}
