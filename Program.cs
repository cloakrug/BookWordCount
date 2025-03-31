using BookWordCount.Helpers;
using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

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

//builder.Services.Configure<JsonOptions>(options =>
//{
//    options.SerializerOptions.PropertyNameCaseInsensitive = true;
//});

var MyAllowSpecificOrigins = "MyAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://accounts.google.com";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://accounts.google.com",
        ValidateAudience = true,
        ValidAudience = "2187841631-i1nggnmlq66mepnhi12qnavkpcs91sko.apps.googleusercontent.com",
        ValidateLifetime = true
    };
    options.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = context =>
        {
            Console.Write("Authentication Failed");
            return Task.FromResult(false);
        },
        OnForbidden = context =>
        {
            Console.Write("Forbidden");
            return Task.FromResult(false);
        }
    };
});

builder.Services.AddControllers();

builder.Services.AddDbContext<BookContext>(opt =>
{
    if(builder.Configuration.GetValue<bool>("useInMemoryDb"))
    {
        opt.UseInMemoryDatabase("bookdb");
    } else
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        // log connection string
        Console.WriteLine($"Connection string: {connectionString.Substring(0, 50)}");
        opt.UseNpgsql(connectionString);
    }
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserBookStatService, UserBookStatService>();

// TODO: Delete - this is used to seed data while testing.
builder.Services.AddScoped<BookDbInitializer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
} 

//app.UseHttpsRedirection();

// if dev, serve from the Angular dist folder, else, wwwroot
if (app.Environment.IsDevelopment())
{
    var filePathProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist"));

    app.UseDefaultFiles(new DefaultFilesOptions() { FileProvider = filePathProvider });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = filePathProvider,
    });
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles(); // wwwroot
}



app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


// TODO: Delete - this is used to seed data while testing.
if(builder.Configuration.GetValue<bool>("useInMemoryDb"))
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<BookDbInitializer>();
        dbInitializer.SeedDatabase();
    }
}


app.MapHealthChecks("/health");

app.Run();

