var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // No MapOpenApi for now
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
    {
        int randomValue = Random.Shared.Next(-22, 55);
        var weatherData = new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                randomValue,
            new WeatherTextValue(randomValue).checkWeather()
        );

        return weatherData;
    }).ToArray();

    return forecast;
}).WithName("GetWeatherForecast");

app.MapGet("/hello", () => "Hello from my first .NET API!").WithName("Hello");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class WeatherTextValue(int Value)
{
    private string[] summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public string checkWeather()
    {
        if (Value < 0)
        {
            return summaries[0];
        }
        else if (Value < 5)
        {
            return summaries[1];
        }
        else if (Value < 10)
        {
            return summaries[2];
        }
        else if (Value < 15)
        {
            return summaries[3];
        }
        else if (Value < 20)
        {
            return summaries[4];
        }
        else if (Value < 25)
        {
            return summaries[5];
        }
        else if (Value < 30)
        {
            return summaries[6];
        }
        else if (Value < 35)
        {
            return summaries[7];
        }
        else if (Value < 40)
        {
            return summaries[8];
        }
        else
        {
            return summaries[9];
        }
    }

}
