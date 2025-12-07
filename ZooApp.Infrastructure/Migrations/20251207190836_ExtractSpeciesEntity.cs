using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtractSpeciesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedingInterval",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Kingdom",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SpeciesName",
                table: "Animals");

            migrationBuilder.AddColumn<decimal>(
                name: "SpeciesId",
                table: "Animals",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FeedingIntervalInHours = table.Column<int>(type: "integer", nullable: false),
                    Kingdom = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SpeciesId",
                table: "Animals",
                column: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Species_SpeciesId",
                table: "Animals",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Species_SpeciesId",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Animals_SpeciesId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "FeedingInterval",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Kingdom",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpeciesName",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
