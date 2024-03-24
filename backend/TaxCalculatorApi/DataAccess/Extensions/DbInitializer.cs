public static class DbInitializer
{
    public static void EnsureDatabaseSeeded(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}