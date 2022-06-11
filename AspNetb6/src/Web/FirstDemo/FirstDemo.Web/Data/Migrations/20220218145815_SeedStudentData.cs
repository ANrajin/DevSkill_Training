using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstDemo.Web.Migrations.TrainingDb
{
    public partial class SeedStudentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "CGPA", "Name" },
                values: new object[] { -3, "Rajshahi", 2.8999999999999999, "Monir" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "CGPA", "Name" },
                values: new object[] { -2, "Khulna", 3.8999999999999999, "Rashed" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "CGPA", "Name" },
                values: new object[] { -1, "Dhaka", 3.0, "Asif" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
