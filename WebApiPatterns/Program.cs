using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using WebApiPatterns.Behaviors;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");


    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog();

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var assembly = Assembly.GetExecutingAssembly();

    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(assembly);
        cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    // automatic registration of validators
    builder.Services.AddValidatorsFromAssembly(assembly);

    // integration of FluentValidation with ASP.NET Core
    builder.Services.AddFluentValidationAutoValidation();

    // integration of FluentValidation with MediatR
    //builder.Services.AddFluentValidationAutoValidation();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}




