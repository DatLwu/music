using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace demo.Migrations
{
    /// <inheritdoc />
    public partial class Update_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listen_count");

            migrationBuilder.AddColumn<int>(
                name: "ListenCount",
                table: "songs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListenCount",
                table: "songs");

            migrationBuilder.CreateTable(
                name: "Listen_count",
                columns: table => new
                {
                    Listen_countID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SongID = table.Column<int>(type: "integer", nullable: false),
                    ListenCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listen_count", x => x.Listen_countID);
                    table.ForeignKey(
                        name: "FK_Listen_count_songs_SongID",
                        column: x => x.SongID,
                        principalTable: "songs",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listen_count_SongID",
                table: "Listen_count",
                column: "SongID");
        }
    }
}
