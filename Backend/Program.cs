using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; 
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy
            .WithOrigins("http://localhost:5173") // React dev server
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});



builder.Services.AddSingleton<IGameService, GameService>();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();
app.UseRouting();
app.UseCors("AllowReactApp");
app.UseSession();


app.MapControllers();

app.Run();


