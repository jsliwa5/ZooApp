using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZooApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FeedingIntervalInHours = table.Column<int>(type: "integer", nullable: false),
                    Kingdom = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MonthlyHoursLimit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZooKeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    MonthlyHoursLimit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZooKeepers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastTimeFed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastHealthCheck = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VetId = table.Column<int>(type: "integer", nullable: false),
                    AnimalId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DurationInHours = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visit_Vets_VetId",
                        column: x => x.VetId,
                        principalTable: "Vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZooKeeperTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScheduledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    ZooKeeperId = table.Column<int>(type: "integer", nullable: false),
                    TaskType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    TargetAnimalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZooKeeperTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZooKeeperTasks_ZooKeepers_ZooKeeperId",
                        column: x => x.ZooKeeperId,
                        principalTable: "ZooKeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SpeciesId",
                table: "Animals",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_VetId",
                table: "Visit",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_ZooKeeperTasks_ZooKeeperId",
                table: "ZooKeeperTasks",
                column: "ZooKeeperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "ZooKeeperTasks");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Vets");

            migrationBuilder.DropTable(
                name: "ZooKeepers");
        }
    }
}
