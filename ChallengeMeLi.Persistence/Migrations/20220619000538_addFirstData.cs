using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeMeLi.Persistence.Migrations
{
    public partial class addFirstData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Name", "PosX", "PosY" },
                values: new object[,] {
                    { "kenobi", -500m, -200m },
                    { "skywalker", 100m, -100m },
                    { "sato", 500m, 100m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}