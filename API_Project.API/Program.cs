using API_Project.API.Data;
using API_Project.API.Jobs;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

// Hangfire konfig�rasyonu
builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("APIDb")));
builder.Services.AddHangfireServer();

// HttpClient'� singleton olarak ekle
builder.Services.AddHttpClient<ProductJsonJob>();

// Add services to the container.

builder.Services.AddControllers();

// DbContext�i DI container�a ekliyoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("APIDb")));
builder.Configuration.GetConnectionString("APIDb");

// JobScheduler'� DI container'a ekle
builder.Services.AddScoped<JobScheduler>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Scheduler'� ba�lat
using (var scope = app.Services.CreateScope())
{
    var jobScheduler = scope.ServiceProvider.GetRequiredService<JobScheduler>();
    jobScheduler.Schedule();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
