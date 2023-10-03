using Contabilizacao.Data;
using Contabilizacao.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<ApplicationContext>();
builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetService<IConfiguration>();
    return new ApplicationContext(configuration!.GetConnectionString("Default"));
});
// builder.Services.AddScoped(provider =>
// {
//     return new ProductRepository(provider.GetService<ApplicationContext>()!);
// });

builder.Services.AddScoped<ProductRepository>();
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

app.MapControllers();

app.Run();
