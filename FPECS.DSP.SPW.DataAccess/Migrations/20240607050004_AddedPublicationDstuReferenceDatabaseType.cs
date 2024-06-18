using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPECS.DSP.SPW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedPublicationDstuReferenceDatabaseType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "database_type",
                table: "publications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reference",
                table: "publications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "database_type",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "publications");
        }
    }
}
