using System;
using Minimal.Api.Error;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Mime;
using AutoMapper;

namespace Minimal.Api.Endpoints.Item
{
    using FluentValidation;
    using Minimal.Api.Endpoints.Item.Models;

    public static class ItemEndpoints
    {
        public static WebApplication MapItemEndpoints(this WebApplication app)
        {
            app.MapGet(
                    "/api/v1/items",
                     () =>
                    {

                        var items = new List<Item>()
                        {
                            new Item()
                            {
                                Id=1,
                                Name="Test item"
                            }
                        };
                        return Results.Ok<List<Item>>(items);
                    })
                    .WithTags("Items")
                .WithMetadata(new SwaggerOperationAttribute("Query all Items", "\n GET /Items"))
                .Produces<List<Item>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
                .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapGet(
                "/api/v1/items/{id:long}",
                 (long id) =>
                 {
                     var item = new Item()
                     {
                         Id = 1,
                         Name = "Test item"
                     };
                     Results.Ok<Item>(item);
                 })
                    .WithTags("Items")
            .WithMetadata(new SwaggerOperationAttribute("Get item by Id", "\n GET /Item/1"))
            .Produces<Item>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);



            app.MapPost(
                "/api/v1/items",
                 async (HttpRequest httpRequest, IMapper mapper, IValidator<CreateItemRequest> validator, CreateItemRequest request) =>
                {
                    var validationResult = await validator.ValidateAsync(request);

                    if (!validationResult.IsValid) 
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    var item = mapper.Map<Item>(request);

                    //TODO: Add, generate id
                    item.Id = 1;

                    return Results.Created(UriHelper.GetEncodedUrl(httpRequest), item);
                })
                    .WithTags("Items")
            .WithMetadata(new SwaggerOperationAttribute("Create new Item", "\n POST /Items { name: }"))
            .Produces<Item>(StatusCodes.Status201Created, contentType: MediaTypeNames.Application.Json)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);


            app.MapDelete(
                "/api/v1/items/{id:long}",
                 (long id) =>
                {
                    //TODO: Delete item by id

                    return Results.NoContent();
                })
                    .WithTags("Items")
            .WithMetadata(new SwaggerOperationAttribute("Delete Item by Id", "\n DELETE /Items/1"))
            .Produces(StatusCodes.Status204NoContent, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapPut(
                "/api/v1/items/{id:long}",
                async (long id, UpdateItemRequest request, IMapper mapper) =>
                {
                    //TODO: Get by id and update
                    var itemById = new Item
                    {
                        Id = 1,
                        Name = "Test item"
                    };

                    var updatedItem = mapper.Map<Item>(request);
                    return Results.NoContent();
                })
                    .WithTags("Items")
            .WithMetadata(new SwaggerOperationAttribute("Update Item by Id", "\n PUT /Items/1"))
            .Produces<Item>(StatusCodes.Status201Created, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);



            return app;
        }
    }
}

