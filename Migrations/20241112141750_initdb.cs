using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using RazorWeb.Models;

#nullable disable

namespace RazorWeb.Migrations
{
	/// <inheritdoc />
	public partial class initdb : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "post",
				columns: table => new
				{
					id = table.Column<string>(type: "nvarchar(450)", nullable: false),
					title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
					created = table.Column<DateTime>(type: "date", nullable: false),
					content = table.Column<string>(type: "ntext", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("id", x => x.id);
				});

			// Insert data
			// Fake data: Bogus

			Randomizer.Seed = new Random(8675309);

			var faker = new Faker<Article>()
				.RuleFor(a => a.Id, f => f.Random.Guid().ToString())
				.RuleFor(a => a.Title, f => f.Lorem.Sentence(10, 20))
				.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2024, 1, 1), DateTime.Today))
				.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1, 4));

			var articles = faker.Generate(150);

			foreach (var article in articles)
			{
				migrationBuilder.InsertData(
					table: "post",
					columns: new[] { "id", "title", "created", "content" },
					values: new object[] { article.Id, article.Title, article.Created, article.Content });
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
