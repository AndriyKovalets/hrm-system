using Hrm.Infrastructure;
using Hrm.Application;
using Hrm.WebApp.ServiceExtentions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        builder.Services.SetupInfrastructure(builder.Configuration);
        builder.Services.SetupApplication(builder.Configuration);
        builder.Services.SetupWebApp(builder.Configuration);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Dashboard}/{action=Index}/{id?}");

        app.Run();
    }
}