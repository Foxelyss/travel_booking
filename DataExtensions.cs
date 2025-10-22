using Microsoft.EntityFrameworkCore;
using TravelBooking.Data;

namespace TravelBooking;

public static class DataExtensions
{
    public static void MigrateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
        dbContext.Database.Migrate();
    }

}
