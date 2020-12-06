using System;
using System.Formats.Asn1;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BB.Blazor.HostedServices;
using BB.Blazor.Middlewares;
using BB.Blazor.Providers;
using BB.BLL.Interfaces;
using BB.BLL.MappingProfiles;
using BB.BLL.Services;
using BB.DAL.Context;
using BB.DAL.Entities;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BB.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true,
                    optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDbContext<BBContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:BBDBConnection"],
                opts => opts.MigrationsAssembly(typeof(BBContext).Assembly.GetName().Name)));
            
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            services.AddHttpClient();
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<ICheckingBranchService, CheckingBranchService>();
            services.AddTransient<ICreditBranchService, CreditBranchService>();
            services.AddTransient<IDepositBranchService, DepositBranchService>();

            services.AddHostedService<DebtBackgroundService>();
            services.AddHostedService<DepositBackgroundService>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],

                        ValidateLifetime = true,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                        ValidateIssuerSigningKey = true,
                        
                        ClockSkew = TimeSpan.Zero
                    };
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
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
            
            InitializeDatabase(app);
            
        }
        
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            if (scope != null)
            {
                using var context = scope.ServiceProvider.GetRequiredService<BBContext>();
                context.Database.Migrate();
            }
        }
        
    }
}