using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestBus1.Models;


namespace TestBus1.Data
{
    public class UrlShortenerDbContext : DbContext
    {
        public DbSet<ShortUrlModel> ShortUrls { get; set; }

        
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options)
        {

        }
        public DbSet<ShortUrlModel> ShortUrlModels { get; set; }
    }
}
