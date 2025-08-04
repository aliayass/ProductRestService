using Microsoft.EntityFrameworkCore;
using SoapCore;
using SoapProductApi.Data;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL baðlantýsý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=soapdb;Username=postgres;Password=12345"));

// SOAP servis
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSoapCore();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger aktif
app.UseSwagger();
app.UseSwaggerUI();

// SOAP endpoint
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IProductService>("/ProductService.svc", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
});

app.Run();
