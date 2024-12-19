using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Books.DataAccess.Migrations
{
    public partial class RemoveBookImagesCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // BookImages tablosunun silinmesi
            migrationBuilder.DropTable(
                name: "BookImages");

            // Eğer Book ile BookImages arasında foreign key ilişkisi varsa, bunu kaldırabiliriz
            // migrationBuilder.DropForeignKey(
            //     name: "FK_BookImages_Books_BookId",
            //     table: "BookImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // BookImages tablosunu yeniden oluşturuyoruz
            migrationBuilder.CreateTable(
                name: "BookImages",
                columns: table => new
                {
                    BookImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookImages", x => x.BookImageId);
                    table.ForeignKey(
                        name: "FK_BookImages_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
