using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Covoiturage_PFA.Migrations
{
    /// <inheritdoc />
    public partial class latmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatriculeCar",
                table: "Trajets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "deletedOrnotdispo",
                table: "Trajets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Commentrating",
                table: "TrajetPassagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserRating",
                table: "TrajetPassagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Numpermit",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "deleted",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DemandeProfils",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact = table.Column<int>(type: "int", nullable: false),
                    Numdepermit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandeProfils", x => x.id);
                    table.ForeignKey(
                        name: "FK_DemandeProfils_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemandeProfils_StatusId",
                table: "DemandeProfils",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandeProfils");

            migrationBuilder.DropColumn(
                name: "MatriculeCar",
                table: "Trajets");

            migrationBuilder.DropColumn(
                name: "deletedOrnotdispo",
                table: "Trajets");

            migrationBuilder.DropColumn(
                name: "Commentrating",
                table: "TrajetPassagers");

            migrationBuilder.DropColumn(
                name: "UserRating",
                table: "TrajetPassagers");

            migrationBuilder.DropColumn(
                name: "Numpermit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "AspNetUsers");
        }
    }
}
