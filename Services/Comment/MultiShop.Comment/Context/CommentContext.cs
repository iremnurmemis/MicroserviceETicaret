using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Context
{
    public class CommentContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1442;initial Catalog=MultiShopCommentDB,integrated Security=true;TrustServerCertificate=True;User=sa;Password=123456aA*");

        }
        public DbSet<UserComment> UserComments { get; set; }
    }
}
