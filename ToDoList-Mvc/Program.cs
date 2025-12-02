using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using ToDoList_Mvc.Data;
using ToDoList_Mvc.Repository;

namespace ToDoList_Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ToDoListDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // DI
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<TaskServices>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddSession(); //

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
