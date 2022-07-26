using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisneyApi.Migrations
{
    public partial class Iniitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qualification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieCharacter",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCharacter", x => new { x.CharacterId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MovieCharacter_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCharacter_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGender",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGender", x => new { x.GenderId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MovieGender_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGender_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "Age", "History", "Image", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, 23, "Es un personaje creado por los estadounidenses Stan Lee y Steve Ditko", "Url de la imagen", "Hombre Araña", 60 },
                    { 2, 18, "Es el personaje principal de la saga Harry Potter", "Url de la imagen", "Harry Potter", 74 },
                    { 3, 55, "es un superhéroe que aparece en los cómics estadounidenses publicados por Marvel Comics.", "Url de la imagen", "Iron Man", 120 },
                    { 4, 23, "Hulk es un personaje ficticio, un superhéroe que aparece en los cómics estadounidenses publicados por la editorial Marvel Comics,", "Url de la imagen", "Hulk", 200 },
                    { 5, 18, "Hermione Jean Granger es un personaje de ficción y una de los tres protagonistas principales de la serie de libros de Harry Potter, publicados por J. K.", "Url de la imagen", "Hermione Granger", 80 }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Url de la imagen", "Fantasia" },
                    { 2, "Url de la imagen", "Accion" },
                    { 3, "Url de la imagen", "Drama" },
                    { 4, "Url de la imagen", "Comedia" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "CreationDate", "Image", "Qualification", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2007, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Url de la imagen", 8, "Spiderman" },
                    { 2, new DateTime(2005, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Url de la imagen", 9, "Harry Potter 4" },
                    { 3, new DateTime(2012, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Url de la imagen", 6, "Avengers" }
                });

            migrationBuilder.InsertData(
                table: "MovieCharacter",
                columns: new[] { "CharacterId", "MovieId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 1, 6 },
                    { 3, 3, 3 },
                    { 4, 3, 4 },
                    { 5, 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "MovieGender",
                columns: new[] { "GenderId", "MovieId", "Id" },
                values: new object[,]
                {
                    { 1, 2, 4 },
                    { 2, 1, 2 },
                    { 2, 2, 5 },
                    { 3, 1, 3 },
                    { 3, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCharacter_MovieId",
                table: "MovieCharacter",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGender_MovieId",
                table: "MovieGender",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCharacter");

            migrationBuilder.DropTable(
                name: "MovieGender");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
