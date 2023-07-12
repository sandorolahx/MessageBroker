using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Publisher.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<Message> Messages => Set<Message>();
    }
}
