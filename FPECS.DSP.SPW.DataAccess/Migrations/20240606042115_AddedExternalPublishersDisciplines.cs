using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FPECS.DSP.SPW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedExternalPublishersDisciplines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_publications_publishers_researcher_pseudonyms_pseudonym_id",
                table: "publications_publishers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_publications_publishers",
                table: "publications_publishers");

            migrationBuilder.DropColumn(
                name: "publication_number",
                table: "publications_publishers");

            migrationBuilder.AlterColumn<long>(
                name: "pseudonym_id",
                table: "publications_publishers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "pk_publications_publishers",
                table: "publications_publishers",
                columns: new[] { "publication_id", "publisher_id" });

            migrationBuilder.CreateTable(
                name: "disciplines",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disciplines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publication_external_publishers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publication_id = table.Column<long>(type: "bigint", nullable: false),
                    pseudonym = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publication_external_publishers", x => x.id);
                    table.ForeignKey(
                        name: "fk_publication_external_publishers_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publications_disciplines",
                columns: table => new
                {
                    publication_id = table.Column<long>(type: "bigint", nullable: false),
                    discipline_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publications_disciplines", x => new { x.discipline_id, x.publication_id });
                    table.ForeignKey(
                        name: "fk_publications_disciplines_disciplines_discipline_id",
                        column: x => x.discipline_id,
                        principalTable: "disciplines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publications_disciplines_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_publication_external_publishers_publication_id",
                table: "publication_external_publishers",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_disciplines_publication_id",
                table: "publications_disciplines",
                column: "publication_id");

            migrationBuilder.AddForeignKey(
                name: "fk_publications_publishers_researcher_pseudonyms_pseudonym_id",
                table: "publications_publishers",
                column: "pseudonym_id",
                principalTable: "researcher_pseudonyms",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_publications_publishers_researcher_pseudonyms_pseudonym_id",
                table: "publications_publishers");

            migrationBuilder.DropTable(
                name: "publication_external_publishers");

            migrationBuilder.DropTable(
                name: "publications_disciplines");

            migrationBuilder.DropTable(
                name: "disciplines");

            migrationBuilder.DropPrimaryKey(
                name: "pk_publications_publishers",
                table: "publications_publishers");

            migrationBuilder.AlterColumn<long>(
                name: "pseudonym_id",
                table: "publications_publishers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "publication_number",
                table: "publications_publishers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_publications_publishers",
                table: "publications_publishers",
                columns: new[] { "publication_id", "publisher_id", "pseudonym_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_publications_publishers_researcher_pseudonyms_pseudonym_id",
                table: "publications_publishers",
                column: "pseudonym_id",
                principalTable: "researcher_pseudonyms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
