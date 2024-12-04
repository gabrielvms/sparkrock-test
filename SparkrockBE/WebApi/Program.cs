using Common.Extensions;
using Common.Helpers;
using WebApi;

const string corsPolicy = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

// Add builder configs
builder.AddEnvironmentConfig();
builder.AddLogging();

//Add services to the container.
builder.Services.AddServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy => policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader());
});

// Configure the HTTP request pipeline
var app = builder.Build();
app.UseCors(corsPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(builder => ErrorHandler.UseCustomErrors(builder, app.Environment));
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
