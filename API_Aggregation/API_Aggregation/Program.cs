using API_Aggregation;
using API_Aggregation.Services;
using API_Aggregation.Services.Implementations;
using API_Aggregation.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddScoped<IGitHubService, GitHubService>();

builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();



app.MapControllers();

app.Run();

