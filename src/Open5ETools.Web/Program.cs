using Open5ETools.Core;
using Open5ETools.Infrastructure;
using Open5ETools.Infrastructure.Data;
using Open5ETools.Web;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddProblemDetails();
builder.Services.AddWebServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

builder.Host.AddSerilog(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var initializer = scope.ServiceProvider
            .GetRequiredService<AppDbContextInitializer>();
        using var source = new CancellationTokenSource();
        var token = source.Token;
        await initializer.UpdateAsync(token);
        await initializer.SeedDataAsync(token);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider
            .GetRequiredService<ILogger<Program>>();

        logger.LogError(ex,
            "An error occurred during database initialization.");

        throw;
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHealthChecks("/health");

app.Run();
