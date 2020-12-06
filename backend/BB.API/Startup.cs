using System;
using System.Text;
using AutoMapper;
using BB.API.HostedServices;
using BB.API.Middlewares;
using BB.BLL.Interfaces;
using BB.BLL.MappingProfiles;
using BB.BLL.Services;
using BB.DAL.Context;
using BB.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BC = BCrypt.Net.BCrypt;


namespace BB.API
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo {Title = "BB.API", Version = "v1"});
                opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    In = ParameterLocation.Header
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Scheme = "oauth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddDbContext<BBContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:BBDBConnection"],
                opts => opts.MigrationsAssembly(typeof(BBContext).Assembly.GetName().Name)));

            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<ICheckingBranchService, CheckingBranchService>();
            services.AddTransient<ICreditBranchService, CreditBranchService>();
            services.AddTransient<IDepositBranchService, DepositBranchService>();

            services.AddHostedService<DebtBackgroundService>();
            services.AddHostedService<DepositBackgroundService>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BB.API v1"));
            }

            app.UseCors("AnyOrigin");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            InitializeDatabase(app);
            
            SeedDatabase(app);

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
        
        
        private static void SeedDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<BBContext>();

            context.Database.EnsureCreated();

            var user1 = new User()
            {
                UserId = 1,
                FirstName = "Alex",
                LastName = "Slobozhenko"
            };

            var user2 = new User()
            {
                UserId = 2,
                FirstName = "John",
                LastName = "Travolta"
            };

            var user3 = new User()
            {
                UserId = 3,
                FirstName = "Bill",
                LastName = "Gates"
            };

            var checkBranch1 = new CheckingBranch()
            {
                CheckingBranchId = 1,
                Balance = 1000
            };
            
            var checkBranch2 = new CheckingBranch()
            {
                CheckingBranchId = 2,
                Balance = 100
            };
            
            var checkBranch3 = new CheckingBranch()
            {
                CheckingBranchId = 3,
                Balance = 250
            };

            var creditBranch1 = new CreditBranch()
            {
                CreditBranchId = 1,
                Available = 1000,
                Balance = 1000,
            };
            
            var creditBranch2 = new CreditBranch()
            {
                CreditBranchId = 2,
                Available = 500,
                Balance = 500,
            };
            
            var creditBranch3 = new CreditBranch()
            {
                CreditBranchId = 3,
                Available = 100,
                Balance = 10,
            };

            var depositBranch1 = new DepositBranch()
            {
                DepositBranchId = 1
            };
            
            var depositBranch2 = new DepositBranch()
            {
                DepositBranchId = 2
            };
            
            var depositBranch3 = new DepositBranch()
            {
                DepositBranchId = 3
            };
            
            
            var cards1 = new Card()
            {
                CardId = 1,
                Pin = BC.HashPassword("1111"),
                IsBlocked = false,
                UserId = 1,
                CheckingBranchId = 1,
                CreditBranchId = 1,
                DepositBranchId = 1,
                Number = "1111"
            };

            var cards2 = new Card()
            {
                CardId = 2,
                Pin = BC.HashPassword("2222"),
                IsBlocked = false,
                UserId = 2,
                CheckingBranchId = 2,
                CreditBranchId = 2,
                DepositBranchId = 2,
                Number = "2222"
            };

            var cards3 = new Card()
            {
                CardId = 3,
                Pin = BC.HashPassword("3333"),
                IsBlocked = false,
                UserId = 3,
                CheckingBranchId = 3,
                CreditBranchId = 3,
                DepositBranchId = 3,
                Number = "3333"
            };
            
            
            context.Cards.Add(cards1);
            context.Cards.Add(cards2);
            context.Cards.Add(cards3);

            
            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);

            context.CheckingBranches.Add(checkBranch1);
            context.CheckingBranches.Add(checkBranch2);
            context.CheckingBranches.Add(checkBranch3);

            context.CreditBranches.Add(creditBranch1);
            context.CreditBranches.Add(creditBranch2);
            context.CreditBranches.Add(creditBranch3);

            context.DepositBranches.Add(depositBranch1);
            context.DepositBranches.Add(depositBranch2);
            context.DepositBranches.Add(depositBranch3);
            
            context.SaveChanges();
        }
    }
}