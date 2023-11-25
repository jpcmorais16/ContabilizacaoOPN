using Contabilizacao.Data;
using Contabilizacao.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(b => b.AddConsole());

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetService<IConfiguration>();
    return new ApplicationContext(configuration!.GetConnectionString("Default"));
});

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped(provider =>
{
    return new UserRepository(provider.GetService<ApplicationContext>()!);
});

builder.Services.AddScoped(provider =>
{
    return new SupermarketRepository(provider.GetService<ApplicationContext>()!);
});

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => 

    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()

));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error != null)
        {
            await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
        }
    });
});

app.UseCors("corspolicy");

app.MapControllers();

app.Run();
