using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTechnik.Data.Migrations
{
    
    public partial class UpdateObjectEntityFirma : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Objects",
                newName: "Firma");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firma",
                table: "Objects",
                newName: "Company");
        }
    }
}
