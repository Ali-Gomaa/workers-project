using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workers.Migrations
{
    /// <inheritdoc />
    public partial class AddJobTitleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobTitleId",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_JobTitleId",
                table: "People",
                column: "JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_JobTitles_JobTitleId",
                table: "People",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_JobTitles_JobTitleId",
                table: "People");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropIndex(
                name: "IX_People_JobTitleId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "People");
        }
    }
}
