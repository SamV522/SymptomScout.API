using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymptomScout.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    DiagnosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.DiagnosisId);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    SymptomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.SymptomId);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisSymptom",
                columns: table => new
                {
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    SymptomId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisSymptomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisSymptom", x => new { x.DiagnosisId, x.SymptomId });
                    table.ForeignKey(
                        name: "FK_DiagnosisSymptom_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosisSymptom_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "SymptomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisSymptom_DiagnosisId_SymptomId",
                table: "DiagnosisSymptom",
                columns: new[] { "DiagnosisId", "SymptomId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisSymptom_SymptomId",
                table: "DiagnosisSymptom",
                column: "SymptomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosisSymptom");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Symptoms");
        }
    }
}
