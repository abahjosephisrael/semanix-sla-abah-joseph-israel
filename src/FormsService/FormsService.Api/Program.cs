using FormsEngine.Shared.Extensions;
using FormsService.Infrastructure;
using Prometheus;
using FormsService.Application;

var builder = WebApplication.CreateBuilder(args);
WebApplicationBuilderExtensions.ConfigureHost(builder);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSharedMiddlewares();
app.UseMetricServer();
app.Run();
