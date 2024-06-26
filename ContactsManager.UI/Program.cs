using Serilog;
using ContactsManager.UI.Middleware;
using ContactsManager.UI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.
    ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseHttpLogging();

if (!builder.Environment.IsEnvironment("Test"))
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
