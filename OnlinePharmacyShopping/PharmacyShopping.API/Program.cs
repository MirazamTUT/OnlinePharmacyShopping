using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.BusinessLogic.ServiceExtentions;

var builder = WebApplication.CreateBuilder(args);

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
