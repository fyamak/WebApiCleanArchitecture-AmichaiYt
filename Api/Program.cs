using Application;
using Application.Services.Authentication;
using Infrastructure;
using System;


var builder = WebApplication.CreateBuilder(args);
{
    // Adding Services to the Dependency Injection Container
    builder.Services
        .AddAplication()
        .AddInfrastructure(); // This method is an extension method that adds application services to the DI container.
    
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

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

    app.UseHttpsRedirection(); // Forces requests to use HTTPS instead of HTTP.
    //app.UseAuthorization();
    app.MapControllers(); //Maps incoming HTTP requests to the appropriate controller actions.
}



app.Run();
