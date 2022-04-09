using BookWordCount.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BookContext>(opt => 
    opt.UseInMemoryDatabase("bookdb"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// TODO: Delete - this is used to seed data while testing.
builder.Services.AddScoped<BookDbInitializer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


// TODO: Delete - this is used to seed data while testing.
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<BookDbInitializer>();
    dbInitializer.SeedDatabase();
}


app.Run();

