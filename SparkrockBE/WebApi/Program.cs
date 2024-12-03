using Common.Middlewares;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnvironmentConfig();
builder.AddLogging();

builder.Services.AddServices(builder.Configuration, builder.Environment);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
