using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.ServiceExtentions;
using PharmacyShopping.DataAccess.DbConnection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
var config = builder.Configuration.GetSection("ConnectionStrings");
builder.Services.AddDbContext<PharmacyDbContext>(option => option.UseNpgsql(config["DefaultConnect"]));
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddScoped<PharmacyDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();