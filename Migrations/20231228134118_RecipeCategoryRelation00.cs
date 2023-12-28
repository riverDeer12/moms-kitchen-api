using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MomsKitchen.Migrations
{
    /// <inheritdoc />
    public partial class RecipeCategoryRelation00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Recipes_RecipeId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RecipeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryRecipe",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipesRecipeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRecipe", x => new { x.CategoriesCategoryId, x.RecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_CategoryRecipe_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryRecipe_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRecipe_RecipesRecipeId",
                table: "CategoryRecipe",
                column: "RecipesRecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRecipe");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RecipeId",
                table: "Categories",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Recipes_RecipeId",
                table: "Categories",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }
    }
}
