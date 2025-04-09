using Api.Common.Mapper;
using Api.Middleware;
using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
{
    // Adding Services to the Dependency Injection Container
    builder.Services
        .AddAplication()
        .AddInfrastructure(builder.Configuration); // This method is an extension method that adds application services to the DI container.
    
    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(MappingProfile));


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer", In = ParameterLocation.Header, Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
            },
            new List<string>()
        }
    });
    });

}




// Building the Application
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection(); // Forces requests to use HTTPS instead of HTTP.
    app.UseAuthentication(); // Adds authentication middleware to the pipeline. Find the correct authentication scheme to use.
    app.UseAuthorization(); // Adds authorization middleware to the pipeline. This middleware checks if the user is authorized to access the requested resource.
    app.MapControllers(); //Maps incoming HTTP requests to the appropriate controller actions.
}



app.Run();
