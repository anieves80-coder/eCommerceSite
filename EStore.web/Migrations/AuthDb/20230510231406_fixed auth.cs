using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class fixedauth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cd09cc0-de01-4214-b872-a907284595ed",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb38863a-9bd2-498c-bb11-60f7dad764f4", "ADMIN@ESTORE.COM", "ADMIN", "AQAAAAEAACcQAAAAEDme7Y8hUABaaJL1gQvbuQg3cM6QPPFxcckUoleq3kDbbe0jqTzVb4V6sEuaRgYjCA==", "039bd1bd-e12c-4984-a629-8755549b41b9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cd09cc0-de01-4214-b872-a907284595ed",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39a90b7e-7bca-4366-ba64-7b432d6e7cb1", null, null, "AQAAAAEAACcQAAAAEIYnzRlZbRDcqiDykve8mCqnhG1RDW1Zt4yX4ayOTLT1X15ipjBd2NKfpKXiHopzlA==", "c736adbc-3cde-4c54-b90e-5bc26f40ced0" });
        }
    }
}
