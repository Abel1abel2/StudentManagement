using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_167 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gpa",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "CourseName");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_CourseId",
                table: "Registers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_StudentId",
                table: "Registers",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Courses_CourseId",
                table: "Registers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Students_StudentId",
                table: "Registers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Courses_CourseId",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Students_StudentId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_CourseId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_StudentId",
                table: "Registers");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Courses",
                newName: "Title");

            migrationBuilder.AddColumn<double>(
                name: "Gpa",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
