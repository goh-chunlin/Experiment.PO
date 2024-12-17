using System.Globalization;
using Experiment.PO;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddPortableObjectLocalization(options => 
    options.ResourcesPath = "Localisation");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = LocaleConstants.SupportedAppLocale
        .Select(cul => new CultureInfo(cul))
        .ToArray();

    options.DefaultRequestCulture = new RequestCulture(
        culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.AddInitialRequestCultureProvider(
        new CustomRequestCultureProvider(async httpContext =>
        {
            var currentCulture = 
                CultureInfo.InvariantCulture.Name;
            var requestUrlPath = 
                httpContext.Request.Path.Value;

            if (httpContext.Request.Query.ContainsKey("locale"))
            {
                currentCulture =         
httpContext.Request.Query["locale"].ToString();
            }

            return await Task.FromResult(
                new ProviderCultureResult(currentCulture));
        })
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (IStringLocalizer<WeatherForecast> stringLocalizer) =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            stringLocalizer["weather_" + summaries[Random.Shared.Next(summaries.Length)]]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}