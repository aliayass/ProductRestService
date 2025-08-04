using Microsoft.EntityFrameworkCore;
using ProductSOAPnew.Data;
using ProductSOAPnew.SOAP;
using SoapCore;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductSoapService>();

builder.Services.AddSoapCore();
builder.Services.AddMvc(); // zorunlu çünkü SOAP endpoint MVC pipeline'a eklenir

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IProductService>("/ProductSoapService.svc",
        new SoapEncoderOptions(),
        SoapSerializer.DataContractSerializer); // ya da SoapSerializer.XmlSerializer
});

app.Run();
