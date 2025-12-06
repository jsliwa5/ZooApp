using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedZooKeeper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ZooKeeperTasks_ZooKeeperId",
                table: "ZooKeeperTasks",
                column: "ZooKeeperId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZooKeeperTasks_ZooKeepers_ZooKeeperId",
                table: "ZooKeeperTasks",
                column: "ZooKeeperId",
                principalTable: "ZooKeepers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZooKeeperTasks_ZooKeepers_ZooKeeperId",
                table: "ZooKeeperTasks");

            migrationBuilder.DropIndex(
                name: "IX_ZooKeeperTasks_ZooKeeperId",
                table: "ZooKeeperTasks");
        }
    }
}
