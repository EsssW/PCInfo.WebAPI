using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PCInfo.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pcInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PCIPV4 = table.Column<string>(type: "text", nullable: true),
                    PCName = table.Column<string>(type: "text", nullable: true),
                    OSVersion = table.Column<string>(type: "text", nullable: true),
                    SystemBitRate = table.Column<string>(type: "text", nullable: true),
                    SystemCatalogPath = table.Column<string>(type: "text", nullable: true),
                    CPUModel = table.Column<string>(type: "text", nullable: true),
                    CPUName = table.Column<string>(type: "text", nullable: true),
                    CPUManufacturerer = table.Column<string>(type: "text", nullable: true),
                    CPUNMaxClockSpeed = table.Column<string>(type: "text", nullable: true),
                    CPUKernelCount = table.Column<int>(type: "integer", nullable: false),
                    DriveCount = table.Column<int>(type: "integer", nullable: false),
                    RAMCount = table.Column<int>(type: "integer", nullable: false),
                    TotalRAM = table.Column<string>(type: "text", nullable: true),
                    ScreenCount = table.Column<int>(type: "integer", nullable: false),
                    HResol = table.Column<string>(type: "text", nullable: true),
                    WResol = table.Column<string>(type: "text", nullable: true),
                    VideoCardName = table.Column<string>(type: "text", nullable: true),
                    VideoCardMemoryAmount = table.Column<string>(type: "text", nullable: true),
                    HDDName = table.Column<string>(type: "text", nullable: true),
                    HDDSize = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pcInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "myDriveInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DriveFormat = table.Column<string>(type: "text", nullable: true),
                    TotalSize = table.Column<string>(type: "text", nullable: true),
                    AvailableFreeSpace = table.Column<string>(type: "text", nullable: true),
                    MyPCInfoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_myDriveInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_myDriveInfos_pcInfos_MyPCInfoId",
                        column: x => x.MyPCInfoId,
                        principalTable: "pcInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_myDriveInfos_MyPCInfoId",
                table: "myDriveInfos",
                column: "MyPCInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "myDriveInfos");

            migrationBuilder.DropTable(
                name: "pcInfos");
        }
    }
}
