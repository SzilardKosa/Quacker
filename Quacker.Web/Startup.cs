using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quacker.Dal;
using Quacker.Dal.Entities;
using Quacker.Dal.SeedInterfaces;
using Quacker.Dal.SeedService;
using Quacker.Dal.Services;
using Quacker.Web.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuackerDbContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString(nameof(QuackerDbContext))));

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<QuackerDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(options => {
                IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
            });

            // SMTP szerver beállításokat felolvassa az appsettings.json-ból a MailSettings osztályba.
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailSender, Services.EmailSender>();

            services.AddScoped<IRoleSeedService, RoleSeedService>()
                .AddScoped<IUserSeedService, UserSeedService>();

            services.AddScoped<PostService>()
                    .AddScoped<UserService>()
                    .AddScoped<CommentService>()
                    .AddScoped<MessageService>();

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/User");
            });
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
