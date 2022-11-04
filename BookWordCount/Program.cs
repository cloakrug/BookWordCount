using BookWordCount.Helpers;
using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add services to the container.

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {
//            builder.WithOrigins("https://localhost:3000")
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//        });
//});

builder.Services.AddControllers();

builder.Services.AddDbContext<BookContext>(opt =>
{
    opt.UseInMemoryDatabase("bookdb");
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IWordCountService, WordCountService>();

// TODO: Delete - this is used to seed data while testing.
builder.Services.AddScoped<BookDbInitializer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


// TODO: Delete - this is used to seed data while testing.
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<BookDbInitializer>();
    dbInitializer.SeedDatabase();
}


app.Run();

