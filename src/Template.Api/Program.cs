using Asp.Versioning;
using Asp.Versioning.Conventions;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Template.Core.Item;
using Template.Infrastructure.Data;
using Template.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Template.Api;

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
                    })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration["CorsHosts"]?.Split(";") ?? Array.Empty<string>())
               .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    }));

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{

    var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

    // integrate xml comments
    options.IncludeXmlComments(filePath);
});

builder.Services.AddApplicationDbContext(databaseConnectionString);
builder.Services.AddRepository<IItemRepository, ItemRepository>();

var app = builder.Build();

await app.CreateOrUpdateDatabaseAsync(databaseConnectionString, app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }

        options.InjectStylesheet("/v1/swaggerstyles/");
    });
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
