using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class fixcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ Invoice table

            migrationBuilder.AddColumn<string>(
                name: "ClientTin",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTaxInclusive",
                table: "Invoices",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Invoices",
                type: "numeric(18,2)",
                nullable: true);


            // ✅ Contact table

            migrationBuilder.AddColumn<string>(
                name: "CustomerTin",
                table: "Contacts",
                type: "text",
                nullable: true);
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Invoice table rollback
            migrationBuilder.DropColumn(
                name: "ClientTin",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalTaxInclusive",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Invoices");


            // Contact table rollback
            migrationBuilder.DropColumn(
                name: "CustomerTin",
                table: "Contacts");
        }
    }
}
