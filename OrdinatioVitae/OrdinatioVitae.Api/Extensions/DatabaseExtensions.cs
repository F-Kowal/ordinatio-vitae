using Microsoft.EntityFrameworkCore;
using OrdinatioVitae.Api.Database;

namespace OrdinatioVitae.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await dbContext.Database.MigrateAsync();
            app.Logger.LogInformation("Database migrations applied successfully.");
        }
        catch (Exception e)
        { 
            app.Logger.LogInformation(e, "An error occurred while migrating the database.");
            throw;
        }
    }
}
