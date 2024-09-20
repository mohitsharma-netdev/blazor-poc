using BlazorServerAppDemo.Components;
using BlazorServerAppDemo.Middleware;
using BlazorServerAppDemo.Services;

namespace BlazorServerAppDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Register the TodoService (depedency injections, configuring services)
            builder.Services.AddScoped<ITodoService, TodoService>();

            builder.Services.AddScoped(sp => new HttpClient ());

            var app = builder.Build();

            // Use CustomMiddleware
            app.UseMiddleware<CustomMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
