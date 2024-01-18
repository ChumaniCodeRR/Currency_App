using Currency_Exchange_Manager_App.Model;
using Currency_Exchange_Manager_App.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICacheService, CacheService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Currency_Exchange_Manager_App",
        Version = "v1"
    });
});
builder.Services.AddTransient<DataAppContext>();
builder.Services.AddTransient<ICurrencyInfo, CurrencyRepo>();
builder.Services.AddTransient<IConversionCurrency, ConversionCurrencyRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency_Exchange_Manager_App v1"));

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

/*app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});*/

app.Run();
