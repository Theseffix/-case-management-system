using ITHSManagement.Data;
using ITHSManagement.Models;
using ITHSManagement.Models.Entities;
using ITHSManagement.Services;
using ITHSManagement.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ITHSManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextFactory<EFContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<EFContext>();

            services.AddScoped<EFContext>(p => p.GetRequiredService<IDbContextFactory<EFContext>>().CreateDbContext());

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 2;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzåäöABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-@åÄÖ.";
                options.User.RequireUniqueEmail = false;
            });
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<StudentRepository>();
            services.AddScoped<ContactRepository>();
            services.AddScoped<AddressRepository>();

            services.AddScoped<IProgrammeService,ProgrammeService>();
            services.AddScoped<CourseService>();
            services.AddScoped<Course>();

            services.AddScoped<StudentService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<TaskRepository>();
            services.AddScoped<TaskCommentRepository>();
            services.AddScoped<FileIO>();
            services.AddScoped<TodoRepository>();
            services.AddScoped<ProgrammeRepository>();
            services.AddScoped<CourseRepository>();
            services.AddScoped<GroupRepository>();
            services.AddScoped<GroupService>();
            services.AddScoped<TaskService>();
            services.AddScoped<NavMenu>();
            services.AddLogging();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
