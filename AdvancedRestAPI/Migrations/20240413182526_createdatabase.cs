using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedRestAPI.Migrations
{
    /// <inheritdoc />
    public partial class createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: true),
                    BloodGroup = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
