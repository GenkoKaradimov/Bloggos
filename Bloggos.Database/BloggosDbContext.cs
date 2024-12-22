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
        }
    }
}
