using Common.Models;
using Microsoft.EntityFrameworkCore;
using Publisher.Data;

namespace Publisher.Test
{
    public static class ContextGenerator
    {
        public static AppDataContext Generate()
        {
            var optionBuilder = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AppDataContext(optionBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }

        public async static Task Seed(AppDataContext context)
        {
            await context.Topics.AddAsync(new Topic() { Id = 1, Name = "Test Topic" });
            await context.SaveChangesAsync();
        }
    }
}
