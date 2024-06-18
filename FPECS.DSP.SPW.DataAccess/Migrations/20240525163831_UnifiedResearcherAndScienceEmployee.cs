using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FPECS.DSP.SPW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UnifiedResearcherAndScienceEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "science_employee");

            migrationBuilder.DropColumn(
                name: "npp_id",
                table: "researchers");

            migrationBuilder.RenameColumn(
                name: "orc_id",
                table: "researchers",
                newName: "chair_id");

            migrationBuilder.AddColumn<int>(
                name: "academic_degree",
                table: "researchers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "researchers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "orcid_url",
                table: "researchers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "researchers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "posada",
                table: "researchers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stepin",
                table: "researchers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zvannya",
                table: "researchers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_researchers_chair_id",
                table: "researchers",
                column: "chair_id");

            migrationBuilder.AddForeignKey(
                name: "fk_researchers_chairs_chair_id",
                table: "researchers",
                column: "chair_id",
                principalTable: "chairs",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_researchers_chairs_chair_id",
                table: "researchers");

            migrationBuilder.DropIndex(
                name: "ix_researchers_chair_id",
                table: "researchers");

            migrationBuilder.DropColumn(
                name: "academic_degree",
                table: "researchers");

            migrationBuilder.DropColumn(
                name: "email",
                table: "researchers");

            migrationBuilder.DropColumn(
                name: "orcid_url",
                table: "researchers");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "researchers");

            migrationBuilder.DropColumn(
                name: "posada",
                table: "researchers");

            migrationBuilder.DropColumn(
                name: "stepin",
                table: "researchers");

            migrationBuilder.DropColumn(
                name: "zvannya",
                table: "researchers");

            migrationBuilder.RenameColumn(
                name: "chair_id",
                table: "researchers",
                newName: "orc_id");

            migrationBuilder.AddColumn<long>(
                name: "npp_id",
                table: "researchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "science_employee",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chair_id = table.Column<long>(type: "bigint", nullable: false),
                    academic_degree = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    orcid_url = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    posada = table.Column<string>(type: "text", nullable: false),
                    stepin = table.Column<string>(type: "text", nullable: false),
                    zvannya = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_science_employee", x => x.id);
                    table.ForeignKey(
                        name: "fk_science_employee_chairs_chair_id",
                        column: x => x.chair_id,
                        principalTable: "chairs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_science_employee_chair_id",
                table: "science_employee",
                column: "chair_id");
        }
    }
}
