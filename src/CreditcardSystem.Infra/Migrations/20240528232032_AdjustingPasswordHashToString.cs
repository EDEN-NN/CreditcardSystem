using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditcardSystem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingPasswordHashToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(32)",
                oldMaxLength: 32);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "users",
                type: "varbinary(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "users",
                type: "varbinary(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }
    }
}
