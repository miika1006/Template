using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Globalization;
using System.Net;
using Serilog;
using Serilog.Enrichers;
using FluentValidation;
using AutoMapper;
using Minimal.Api.Error;
using Minimal.Api.Endpoints.Item;
using Minimal.Api.Endpoints.Item.Models;
using Minimal.Api.Endpoints.Item.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
{
    var assembly = Assembly.GetEntryAssembly();


    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
            .Enrich.WithProperty("Assembly Version", assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? "");
});
builder.Services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    opt.SerializerOptions.PropertyNameCaseInsensitive = true;
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

builder.Services.AddHttpContextAccessor();

var ti = CultureInfo.CurrentCulture.TextInfo;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = $"Template.Api API v1 - {ti.ToTitleCase(builder.Environment.EnvironmentName)} ",
            Description = "Template implementation of Minimal API in .NET.",
            Contact = new OpenApiContact
            {
                Name = "Template.Api API",
                Url = new Uri("https://github.com/")
            },
            License = new OpenApiLicense()
            {
                Name = "Template.Api API - License - MIT",
                Url = new Uri("https://opensource.org/licenses/MIT")
            },
            TermsOfService = new Uri("https://github.com/")
        });
 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.EnableAnnotations();
    options.DocInclusionPredicate((name, api) => true);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);


builder.Services.AddScoped<IValidator<CreateItemRequest>, CreateItemValidator>();

var app = builder.Build();
app.UseExceptionHandler(appError => appError.Run(async context =>
{
    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

    if (contextFeature != null)
    {

        // Set the Http Status Code
        var statusCode = contextFeature.Error switch
        {
            ValidationException ex => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        // Prepare Generic Error
        var apiError = new ApiError(contextFeature.Error.Message, contextFeature.Error?.InnerException?.Message ?? "", contextFeature.Error?.StackTrace ?? "");

        // Set Response Details
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        // Return the Serialized Generic Error
        await context.Response.WriteAsync(JsonSerializer.Serialize(apiError));
    }
}));

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.DocumentTitle = "Template API";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Template.Api - {ti.ToTitleCase(app.Environment.EnvironmentName)} - V1");
    c.InjectStylesheet("/swaggerstyles");
});


app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name ?? "");
    };
});


app.UseHsts();

app.UseHttpsRedirection();


app.MapGet("/swaggerstyles", () => Results.Content(@"
.swagger-ui .topbar{
    display:none
}
.swagger-ui .info {
    margin: 20px 0 50px 0;
}
.swagger-ui section.models{
    display:none;
}", "text/css")).ExcludeFromDescription();


app.MapItemEndpoints();


try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}


