using FIAPCloudGames.Application.Requests;
using FIAPCloudGames.Application.Responses;
using FIAPCloudGames.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FIAPCloudGames.SharedKernel;

namespace FIAPCloudGames.API.Endpoints;

public static class GameEndpoints
{

    public static WebApplication MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", async (IGameService service, [FromQuery] int page = 1, [FromQuery] int pageSize = 10) => {

            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 1;

            if (pageSize > 100)
                pageSize = 100;

            int skip = (page - 1) * pageSize;

            var games = await service.FindAll(skip: skip, take: pageSize);

            return Results.Ok(games?.Select(item => new GetGameResponse { 
                Id = item.Id, 
                Name = item.Name,
                Description = item.Description,
                CategoryDescription = item.Category.GetDescription(),
                LastUpdate = item.LastUpdate,
                Price = item.Price.Value,
                ReleaseDate = item.ReleaseDate
            }));
        }).AllowAnonymous();

        group.MapGet("/{id:guid}", async (IGameService service, [FromRoute] Guid id) => {
            var game = await service.Find(id: id);

            if (game == null)
                return Results.NotFound();

            return Results.Ok(new GetGameResponse
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryDescription = game.Category.GetDescription(),
                LastUpdate = game.LastUpdate,
                Price = game.Price.Value,
                ReleaseDate = game.ReleaseDate
            });
        }).AllowAnonymous();

        group.MapDelete("/{id:guid}", async (IGameService service, [FromRoute] Guid id) => {
            var game = await service.Find(id: id);

            if (game == null)
                return Results.NotFound();

            await service.Delete(id: game.Id);

            return Results.NoContent();
        }).RequireAuthorization("AdminOnly");

        group.MapPost("/", async (IGameService service, [FromBody] CreateGameRequest request) => {
            if (request == null)
                return Results.BadRequest(new GenericMessageResponse { Message = "Invalid body" });

            Guid id = await service.Create(new Domain.Entities.Game(
                name: request.Name, 
                description: request.Description,
                price: request.Price,
                category: request.Category,
                releaseDate: request.ReleaseDate.ToUniversalTime()
            ));

            return Results.Created();
        }).RequireAuthorization("AdminOnly");

        group.MapPatch("/{id:guid}", async (IGameService service, [FromRoute] Guid id, [FromBody] UpdateGameRequest request) => {
            if (request == null)
                return Results.BadRequest(new GenericMessageResponse { Message = "Invalid body" });

            var gameFound = await service.Find(id: id);
            if (gameFound == null)
                return Results.NotFound();

            gameFound.Update(
                name: request.Name,
                description: request.Description,
                price: request.Price,
                category: request.Category,
                releaseDate: request.ReleaseDate?.ToUniversalTime()
            );

            await service.Update(gameFound);

            return Results.Ok();
        }).RequireAuthorization("AdminOnly");


        return app;
    }
}
