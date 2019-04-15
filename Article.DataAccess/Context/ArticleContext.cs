using Article.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.DataAccess.Context
{
    public partial class ArticleContext:DbContext
    {
        private readonly ArticleContext _context;

        public ArticleContext():base("name=ArticleEntities")
        {
            Configuration.LazyLoadingEnabled = false; 
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<Post>().ToTable("Post", "dbo");
            modelBuilder.Entity<Category>().ToTable("Category", "dbo");
            modelBuilder.Entity<Comment>().ToTable("Comment", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
