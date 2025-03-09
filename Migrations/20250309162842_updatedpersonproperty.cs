using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPICVHantering.Migrations
{
    /// <inheritdoc />
    public partial class updatedpersonproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkExperiences_PersonID",
                table: "WorkExperiences");

            migrationBuilder.DropIndex(
                name: "IX_Educations_PersonID",
                table: "Educations");

            migrationBuilder.AlterColumn<string>(
                name: "JobDescription",
                table: "WorkExperiences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_PersonID",
                table: "WorkExperiences",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_PersonID",
                table: "Educations",
                column: "PersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkExperiences_PersonID",
                table: "WorkExperiences");

            migrationBuilder.DropIndex(
                name: "IX_Educations_PersonID",
                table: "Educations");

            migrationBuilder.AlterColumn<string>(
                name: "JobDescription",
                table: "WorkExperiences",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_PersonID",
                table: "WorkExperiences",
                column: "PersonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_PersonID",
                table: "Educations",
                column: "PersonID",
                unique: true);
        }
    }
}
