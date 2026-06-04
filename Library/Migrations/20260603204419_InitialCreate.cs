using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    book_Id = table.Column<string>(type: "text", nullable: false),
                    book_name = table.Column<string>(type: "text", nullable: false),
                    book_author = table.Column<string>(type: "text", nullable: false),
                    book_category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.book_Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    member_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_name = table.Column<string>(type: "text", nullable: false),
                    email_id = table.Column<string>(type: "text", nullable: false),
                    phone_no = table.Column<string>(type: "text", nullable: false),
                    member_Status = table.Column<int>(type: "integer", nullable: false),
                    member_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.member_id);
                });

            migrationBuilder.CreateTable(
                name: "Copies",
                columns: table => new
                {
                    copyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    book_Id = table.Column<string>(type: "text", nullable: false),
                    book_copy_Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copies", x => x.copyId);
                    table.ForeignKey(
                        name: "FK_Copies_Books_book_Id",
                        column: x => x.book_Id,
                        principalTable: "Books",
                        principalColumn: "book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    borrow_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    book_copyId = table.Column<int>(type: "integer", nullable: false),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    borrow_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    return_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    borrow_status = table.Column<int>(type: "integer", nullable: false),
                    book_Id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.borrow_id);
                    table.ForeignKey(
                        name: "FK_Borrows_Books_book_Id",
                        column: x => x.book_Id,
                        principalTable: "Books",
                        principalColumn: "book_Id");
                    table.ForeignKey(
                        name: "FK_Borrows_Copies_book_copyId",
                        column: x => x.book_copyId,
                        principalTable: "Copies",
                        principalColumn: "copyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrows_Members_member_id",
                        column: x => x.member_id,
                        principalTable: "Members",
                        principalColumn: "member_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    Borrow_id = table.Column<int>(type: "integer", nullable: false),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    due_status = table.Column<int>(type: "integer", nullable: false),
                    borrow_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.Borrow_id);
                    table.ForeignKey(
                        name: "FK_Fines_Borrows_Borrow_id",
                        column: x => x.Borrow_id,
                        principalTable: "Borrows",
                        principalColumn: "borrow_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fines_Borrows_borrow_id",
                        column: x => x.borrow_id,
                        principalTable: "Borrows",
                        principalColumn: "borrow_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_book_copyId",
                table: "Borrows",
                column: "book_copyId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_book_Id",
                table: "Borrows",
                column: "book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_member_id",
                table: "Borrows",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_book_Id",
                table: "Copies",
                column: "book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_borrow_id",
                table: "Fines",
                column: "borrow_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "Copies");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
