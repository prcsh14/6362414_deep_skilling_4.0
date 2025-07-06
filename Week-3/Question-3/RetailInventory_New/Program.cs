using Microsoft.EntityFrameworkCore;
using RetailInventory_New.Data;
using RetailInventory_New.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5056); // HTTP
    serverOptions.ListenLocalhost(5057, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

builder.Services.AddDbContext<RetailDbContext>(options =>
    options.UseSqlite("Data Source=retail.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Retail Inventory API",
        Version = "v1",
        Description = "API for managing retail inventory products and categories"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Retail Inventory API v1");
    });
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RetailDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

app.MapGet("/", () => "Retail Inventory API is running!");

app.Run();
