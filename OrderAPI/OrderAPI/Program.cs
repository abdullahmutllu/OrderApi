using Microsoft.EntityFrameworkCore;
using OrderAPI.BackgroundService;
using OrderAPI.OrderDbContext;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OrderApiDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),null
   );
});
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddHostedService<SendMailService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
