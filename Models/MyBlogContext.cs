using Microsoft.EntityFrameworkCore;

namespace RazorWeb.Models
{
	public class MyBlogContext : DbContext
	{
		public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
		{
		}

		public DbSet<Article> Articles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("post");
                entity.HasKey(e => e.Id).HasName("id");
                entity.Property(e => e.Id)
                      .HasColumnName("id");
                entity.Property(e => e.Title)
                      .HasColumnName("title")
                      .HasColumnType("nvarchar")
                      .HasMaxLength(255)
                      .IsRequired();
                entity.Property(e => e.Created)
                      .HasColumnName("created")
                      .HasColumnType("date")
                      .IsRequired();
                entity.Property(e => e.Content)
                      .HasColumnName("content")
                      .HasColumnType("ntext");
            });
        }
	}
}
