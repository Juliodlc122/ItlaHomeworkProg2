using Microsoft.EntityFrameworkCore;
using RopaOutlet.Persistence;
using RopaOutlet.Persistence.Repositories;
using RopaOutlet.Application.Interfaces;
using RopaOutlet.Domain;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OutletDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IOutletRepository, OutletRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OutletDbContext>();
    // db.Database.EnsureCreated(); 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();