using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeMeLi.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SatelliteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PosY = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Satellites_Name",
                        column: x => x.Name,
                        principalTable: "Satellites",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SatellitesData",
                columns: table => new
                {
                    SatelliteDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatellitesData", x => x.SatelliteDataId);
                    table.ForeignKey(
                        name: "FK_SatellitesData_Satellites_Name",
                        column: x => x.Name,
                        principalTable: "Satellites",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageWords",
                columns: table => new
                {
                    WordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageWords", x => x.WordId);
                    table.ForeignKey(
                        name: "FK_MessageWords_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Name",
                table: "Messages",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MessageWords_MessageId",
                table: "MessageWords",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_SatellitesData_Name",
                table: "SatellitesData",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageWords");

            migrationBuilder.DropTable(
                name: "SatellitesData");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Satellites");
        }
    }
}
