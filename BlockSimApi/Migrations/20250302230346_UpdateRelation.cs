using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlockSimApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Blocks_BlockId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sentence",
                table: "Blocks",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Blocks_BlockId",
                table: "Transactions",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Blocks_BlockId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "Blocks");

            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Blocks_BlockId",
                table: "Transactions",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id");
        }
    }
}
