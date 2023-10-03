using Contabilizacao.Data;

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
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<SupermarketRepository>();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => 

    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()

));

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
