using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using RazorWeb.Models;

#nullable disable

namespace RazorWeb.Migrations
{
    /// <inheritdoc />
    public partial class V0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created = table.Column<DateTime>(type: "date", nullable: false),
                    content = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            Randomizer.Seed = new Random(8675309);

            var faker = new Faker<Article>();
            faker.RuleFor(x => x.Title, f => f.Lorem.Sentence(5, 10));
            faker.RuleFor(x => x.Created, f => f.Date.Between(new DateTime(2024, 1, 1), DateTime.Today));
            faker.RuleFor(x => x.Content, f => f.Lorem.Paragraphs(1, 4));



            for (int i = 0; i < 150; i++)
            {
                Article article = faker.Generate();
                migrationBuilder.InsertData(
                    table: "post",
                    columns: new[] { "title", "created", "content" },
                    values: new object[] { article.Title, article.Created, article.Content });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post");
        }
    }
}
