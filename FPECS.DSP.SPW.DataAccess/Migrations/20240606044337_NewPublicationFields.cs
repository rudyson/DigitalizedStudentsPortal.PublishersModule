using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPECS.DSP.SPW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewPublicationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publication_origin_source_url",
                table: "publications",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "publication_origin_source",
                table: "publications",
                newName: "publishing_name");

            migrationBuilder.AddColumn<string>(
                name: "conference_city",
                table: "publications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "conference_country",
                table: "publications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "conference_end_date",
                table: "publications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "conference_name",
                table: "publications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "conference_start_date",
                table: "publications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_international",
                table: "publications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_with_student",
                table: "publications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "magazine_issue",
                table: "publications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "magazine_name",
                table: "publications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "magazine_number",
                table: "publications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "page_first",
                table: "publications",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "page_last",
                table: "publications",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "year",
                table: "publications",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "conference_city",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "conference_country",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "conference_end_date",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "conference_name",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "conference_start_date",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "is_international",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "is_with_student",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "magazine_issue",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "magazine_name",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "magazine_number",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "page_first",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "page_last",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "year",
                table: "publications");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "publications",
                newName: "publication_origin_source_url");

            migrationBuilder.RenameColumn(
                name: "publishing_name",
                table: "publications",
                newName: "publication_origin_source");
        }
    }
}
