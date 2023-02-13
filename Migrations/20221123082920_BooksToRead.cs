using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadABook.Migrations
{
    public partial class BooksToRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BooksToRead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksToRead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksToRead_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BooksToRead_Users_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksToRead_BookId",
                table: "BooksToRead",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksToRead_ReaderId",
                table: "BooksToRead",
                column: "ReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksToRead");
        }
    }
}
