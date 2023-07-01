using CompaniesApi.RequestHandlers;
using CompaniesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICompaniesRequestHandler, CompaniesRequestHandler>();
builder.Services.AddScoped<IBackendService, BackendService>();

builder.Services.AddHttpClient<IBackendService, BackendService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendUrl"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
