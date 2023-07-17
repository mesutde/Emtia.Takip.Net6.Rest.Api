using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
               o =>
               {
                   o.SwaggerDoc("v1", new OpenApiInfo
                   {
                       Version = "v1",
                       Title = "My API",
                       Description = "My APIs"
                   });
               }
               );

WebApplicationBuilder builder1 = WebApplication.CreateBuilder(args);

builder1.WebHost.UseKestrel(option => option.AddServerHeader = false);

var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();