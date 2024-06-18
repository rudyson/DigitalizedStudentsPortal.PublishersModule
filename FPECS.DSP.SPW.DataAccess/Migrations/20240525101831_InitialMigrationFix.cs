using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FPECS.DSP.SPW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "faculties",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculties", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    publication_origin_source = table.Column<string>(type: "text", nullable: false),
                    publication_origin_source_url = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    pages = table.Column<short>(type: "smallint", nullable: false),
                    pages_author = table.Column<short>(type: "smallint", nullable: true),
                    doi = table.Column<string>(type: "text", nullable: true),
                    isbn = table.Column<string>(type: "text", nullable: true),
                    issn = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "researchers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    npp_id = table.Column<long>(type: "bigint", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    orc_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_researchers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chairs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    abbreviation = table.Column<string>(type: "text", nullable: false),
                    faculty_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chairs", x => x.id);
                    table.ForeignKey(
                        name: "fk_chairs_faculties_faculty_id",
                        column: x => x.faculty_id,
                        principalTable: "faculties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "researcher_profiles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    researcher_id = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    internal_id = table.Column<string>(type: "text", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_researcher_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_researcher_profiles_researchers_researcher_id",
                        column: x => x.researcher_id,
                        principalTable: "researchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "researcher_pseudonyms",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    researcher_id = table.Column<long>(type: "bigint", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_researcher_pseudonyms", x => x.id);
                    table.ForeignKey(
                        name: "fk_researcher_pseudonyms_researchers_researcher_id",
                        column: x => x.researcher_id,
                        principalTable: "researchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "science_employee",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    posada = table.Column<string>(type: "text", nullable: false),
                    zvannya = table.Column<string>(type: "text", nullable: false),
                    academic_degree = table.Column<int>(type: "integer", nullable: false),
                    stepin = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    orcid_url = table.Column<string>(type: "text", nullable: true),
                    chair_id = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "publications_publishers",
                columns: table => new
                {
                    publication_id = table.Column<long>(type: "bigint", nullable: false),
                    publisher_id = table.Column<long>(type: "bigint", nullable: false),
                    pseudonym_id = table.Column<long>(type: "bigint", nullable: false),
                    publication_number = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publications_publishers", x => new { x.publication_id, x.publisher_id, x.pseudonym_id });
                    table.ForeignKey(
                        name: "fk_publications_publishers_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publications_publishers_researcher_pseudonyms_pseudonym_id",
                        column: x => x.pseudonym_id,
                        principalTable: "researcher_pseudonyms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publications_publishers_researchers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "researchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chairs_faculty_id",
                table: "chairs",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_publishers_pseudonym_id",
                table: "publications_publishers",
                column: "pseudonym_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_publishers_publisher_id",
                table: "publications_publishers",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_researcher_profiles_researcher_id",
                table: "researcher_profiles",
                column: "researcher_id");

            migrationBuilder.CreateIndex(
                name: "ix_researcher_pseudonyms_researcher_id",
                table: "researcher_pseudonyms",
                column: "researcher_id");

            migrationBuilder.CreateIndex(
                name: "ix_science_employee_chair_id",
                table: "science_employee",
                column: "chair_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "publications_publishers");

            migrationBuilder.DropTable(
                name: "researcher_profiles");

            migrationBuilder.DropTable(
                name: "science_employee");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "researcher_pseudonyms");

            migrationBuilder.DropTable(
                name: "chairs");

            migrationBuilder.DropTable(
                name: "researchers");

            migrationBuilder.DropTable(
                name: "faculties");
        }
    }
}
