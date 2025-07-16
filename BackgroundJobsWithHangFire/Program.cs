using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

// Add Hangfire services using the connection string from appsettings.json
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(connectionString));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Optional: Show Hangfire dashboard at /hangfire
app.UseHangfireDashboard(pathMatch: "/dashbored");

// Start Hangfire Server
app.UseHangfireServer();

app.MapControllers();

app.Run();
