using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymptomScout.API.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
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
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    DiagnosesDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    SymptomsSymptomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisSymptom", x => new { x.DiagnosesDiagnosisId, x.SymptomsSymptomId });
                    table.ForeignKey(
                        name: "FK_DiagnosisSymptom_Diagnoses_DiagnosesDiagnosisId",
                        column: x => x.DiagnosesDiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosisSymptom_Symptoms_SymptomsSymptomId",
                        column: x => x.SymptomsSymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "SymptomId",
                        onDelete: ReferentialAction.Cascade);
                    table.UniqueConstraint(
                        name: "UC_DiagnosisSymptom_DiagnosisId_SymptomId",
                        columns: x => new { x.DiagnosesDiagnosisId, x.SymptomsSymptomId }
                        );
                });

            migrationBuilder.CreateIndex(
                name: "UC_Diagnosis__Name",
                table: "Diagnoses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisSymptom_SymptomsSymptomId",
                table: "DiagnosisSymptom",
                column: "SymptomsSymptomId");

            migrationBuilder.CreateIndex(
                name: "UC_Symptom_Name",
                table: "Symptoms",
                column: "Name",
                unique: true);
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
