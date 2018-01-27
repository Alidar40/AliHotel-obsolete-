using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace testMVC.Migrations
{
    public partial class AddCloseParameterToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Customers");
        }
    }
}
