using Bloggos.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.Database
{
    public class BloggosDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<MapLink> MapLinks { get; set; }

        public BloggosDbContext(DbContextOptions<BloggosDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Username);

                entity.Property(user => user.Username)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(article => article.Id);

                entity.Property(article => article.Id)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(image => image.Id);

                entity.Property(image => image.Id)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.HasKey(maps => maps.Id);

                entity.Property(maps => maps.Id)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MapLink>(entity =>
            {
                entity.HasKey(link => link.Id);

                entity.Property(link => link.Id)
                    .ValueGeneratedOnAdd();

                entity
                    .HasOne(link => link.Map)
                    .WithMany(map => map.MapLinks)
                    .HasPrincipalKey(map => map.Id)
                    .HasForeignKey(link => link.MapId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
