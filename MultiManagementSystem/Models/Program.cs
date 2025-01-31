using MultiManagementSystem.Components;
using MultiManagementSystem.Services.Abstraction;
using MultiManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("MultiManagementSystem");
builder.Services.AddDbContext<ManagementSystemDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register application services
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();

builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
builder.Services.AddSingleton<ICompanyService, CompanyService>();

builder.Services.AddSingleton<LogFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapFallbackToFile("index.html");

app.Run();
