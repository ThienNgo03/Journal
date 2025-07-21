using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journal.Migrations
{
    /// <inheritdoc />
    public partial class Exercises_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Gadgets");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Gadgets",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "GadgetId",
                table: "Gadgets",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Gadgets",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Gadgets",
                newName: "GadgetId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Gadgets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
