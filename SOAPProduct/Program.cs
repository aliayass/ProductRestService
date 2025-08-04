using Microsoft.EntityFrameworkCore;
using SoapCore;
using SOAPProduct.Controllers;
using SOAPProduct.Data;
using AutoMapper;
using SoapCore.Mapping; // Profile klasörünü içeren namespace




var builder = WebApplication.CreateBuilder(args);

// PostgreSQL bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// SOAP servisi
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSoapCore();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ProductProfile));

// Authorization sadece gerekiyorsa
builder.Services.AddAuthorization();

var app = builder.Build();

// Swagger geliştirme ortamında aktif
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// HTTPS yönlendirme istiyorsan (Swagger için genelde gerekiyor)
app.UseHttpsRedirection();

// Authorization gerçekten gerekiyorsa
app.UseAuthorization();

// SOAP endpoint tanımı
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IProductService>("/ProductService.svc",
        new SoapEncoderOptions(),
        SoapSerializer.DataContractSerializer);
});

app.Run();
