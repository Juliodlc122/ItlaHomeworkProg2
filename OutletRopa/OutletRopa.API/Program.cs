using Microsoft.EntityFrameworkCore;
using OutletRopa.Application.Interfaces;
using OutletRopa.Application.Services;
using OutletRopa.Domain.Interfaces;
using OutletRopa.Infrastructure;
using OutletRopa.Persistence;
using OutletRopa.Persistence.Contexts; 
using OutletRopa.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddInfrastructureLayer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowBlazor",
		b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors("AllowBlazor");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();