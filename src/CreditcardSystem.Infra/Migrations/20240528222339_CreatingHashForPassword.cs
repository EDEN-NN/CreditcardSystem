using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditcardSystem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreatingHashForPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Username = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(32)", maxLength: 32, nullable: false),
                    PasswordSalt = table.Column<string>(type: "VARCHAR(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Creditcards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CardName = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    CardType = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    CardBill = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creditcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creditcard_Owner",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Creditcards_OwnerId",
                table: "Creditcards",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Creditcards");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
