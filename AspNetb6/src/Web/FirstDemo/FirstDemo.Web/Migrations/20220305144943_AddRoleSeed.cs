using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstDemo.Web.Migrations
{
    public partial class AddRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1bdd8337-3200-46a2-b1e0-2331164ede6f"), "637821101834164904", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d73296f5-3255-4485-bfa0-80a3973e4768"), "637821102434164968", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1bdd8337-3200-46a2-b1e0-2331164ede6f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d73296f5-3255-4485-bfa0-80a3973e4768"));
        }
    }
}
