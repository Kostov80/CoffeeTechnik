using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTechnik.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMontajProfilaktikaFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AvariaDate",
                table: "ServiceRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvariaOpisanie",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MontajDate",
                table: "ServiceRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MontajOpisanie",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProfilaktikaDate",
                table: "ServiceRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilaktikaOpisanie",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvariaDate",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "AvariaOpisanie",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "MontajDate",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "MontajOpisanie",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "ProfilaktikaDate",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "ProfilaktikaOpisanie",
                table: "ServiceRequests");
        }
    }
}
