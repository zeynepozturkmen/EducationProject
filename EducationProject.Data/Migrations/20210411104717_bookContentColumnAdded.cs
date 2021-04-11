using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationProject.Data.Migrations
{
    public partial class bookContentColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastWathedOrderNo",
                table: "UserEducation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookContent",
                table: "EducationContent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastWathedOrderNo",
                table: "UserEducation");

            migrationBuilder.DropColumn(
                name: "BookContent",
                table: "EducationContent");
        }
    }
}
