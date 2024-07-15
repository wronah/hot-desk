using HotDesk.Api;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.RegisterAuthentication(config);
builder.Services.AddAuthorization();

builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.ConfigureOptions();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
