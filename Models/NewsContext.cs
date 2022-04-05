using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace NewsApi.Models
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {
        }

        public DbSet<NewsItem> NewsItems { get; set; } = null!;
    }
}