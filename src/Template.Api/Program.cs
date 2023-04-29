using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Template.Core.Item;
using Template.Infrastructure.Data;
using Template.Infrastructure.Data.Repositories;
var builder = WebApplication.CreateBuilder(args);
var databaseConnectionString = builder.Configuration.GetConnectionString("Database");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(
                    options =>
                    {
                        // reporting api versions will return the headers
                        // "api-supported-versions" and "api-deprecated-versions"
                        options.ReportApiVersions = true;
                        options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    })
                .AddMvc(
                    options =>
                    {
                       
                        // automatically applies an api version based on the name of
                        // the defining controller's namespace
                        options.Conventions.Add(new VersionByNamespaceConvention());
                    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration["CorsHosts"]?.Split(";") ?? Array.Empty<string>())
               .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    }));
builder.Services.AddSwaggerGen();

builder.Services.AddRepository<IItemRepository, ItemRepository>();

var app = builder.Build();

await app.CreateOrUpdateDatabaseAsync(databaseConnectionString, app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
