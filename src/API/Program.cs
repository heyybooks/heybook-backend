using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Books.Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Core.Extensions;
using Swap.Business.DependencyResolvers;
using Microsoft.OpenApi.Models;
using UserManagement.Business.DependencyResolvers.Autofac;




var builder = WebApplication.CreateBuilder(args);

// Autofac Service Provider Factory ekleme (ÖNCEL?KL? OLMALI)
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
    builder.RegisterModule(new AutofacUserManagementModule());
  
    
});



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Swap API", 
        Version = "v1" 
    });
});

//swap extension
builder.Services.AddSwapServices();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS 
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

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swap API V1");
    });
}




app.UseHttpsRedirection();
app.UseRouting(); // UseRouting'i eklemeyi unutmay?n
app.UseCors("AllowAllOrigins"); // CORS politikas?n? buraya ekleyin

app.UseAuthorization();

app.MapControllers();

app.Run();