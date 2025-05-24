namespace FIAPCloudGames.API.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/users");
        group.MapGet("/", () => {
            return new { ok = true };
        });

        return app;
    }
}
