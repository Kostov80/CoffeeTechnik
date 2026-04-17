using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTechnik.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceStatusId",
                table: "ServiceRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServiceStatusId",
                table: "ServiceRequests",
                column: "ServiceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_ServiceStatuses_ServiceStatusId",
                table: "ServiceRequests",
                column: "ServiceStatusId",
                principalTable: "ServiceStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_ServiceStatuses_ServiceStatusId",
                table: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "ServiceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_ServiceStatusId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "ServiceStatusId",
                table: "ServiceRequests");
        }
    }
}
