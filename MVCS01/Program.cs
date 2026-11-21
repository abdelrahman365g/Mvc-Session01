using MVCS01.Middlewares;

namespace MVCS01;

public class Progarm
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseMiddleware<RequestTimingMiddleware>();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.Use(async (context, next) =>
        {
            Console.WriteLine("Hello");
            await next(context);
            Console.WriteLine("Bye");
        });
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "shop",
            pattern: "shop/{id:int}",
            defaults: new { controller = "Products", action = "GetProductById" });

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}