using Common.Extensions;
using Common.Helpers;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnvironmentConfig();
builder.AddLogging();

builder.Services.AddServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(builder => ErrorHandler.UseCustomErrors(builder, app.Environment));
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
