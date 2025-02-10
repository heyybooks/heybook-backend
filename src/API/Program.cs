using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Books.Business.DependencyResolvers.Autofac;
using Swap.Business.DependencyResolvers;
using Swap.DataAccess;
using Microsoft.OpenApi.Models;
using UserManagement.Business.DependencyResolvers.Autofac;
using Core.Mapping;


var builder = WebApplication.CreateBuilder(args);

//Autofac Service Provider Factory ekleme
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutoMapperModule()); // Tüm profillere erişim
    builder.RegisterModule(new AutofacBusinessModule()); // Books Modülü
    builder.RegisterModule(new AutofacUserManagementModule()); // User Modülü**
});

// Swap Service Extension
builder.Services.AddSwapServices();


// Controller, Swagger ve CORS ayarları
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Middleware konfigürasyonu
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swap API V1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.MapControllers();
app.Run();
