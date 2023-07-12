using Common.Models;
using Publisher.Data;

namespace Publisher
{
    public static class Initializer
    {
        public static async Task<WebApplication> Initialize(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            context.Database.EnsureCreated();

            await SeedInitTopic(context);

            return app;
        }

        private static async Task SeedInitTopic(AppDataContext context)
        {
            if (!context.Topics.Any())
            {
                await context.Topics.AddAsync(new Topic() { Id = 1, Name = "Test Topic" });

                await context.SaveChangesAsync();
            }
        }
    }
}
