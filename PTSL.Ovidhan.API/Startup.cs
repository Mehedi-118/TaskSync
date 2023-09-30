using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;

using PTSL.Ovidhan.Api.Helpers;
using PTSL.Ovidhan.Common.Const;
using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Helper;
using PTSL.Ovidhan.DAL.UnitOfWork;

namespace Ovidhan
{
    public class Startup
    {
        private IWebHostEnvironment WebHostEnvironment { get; set; }
        private string UploadDirectory { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
            UploadDirectory = Path.GetFullPath(Path.Combine(WebHostEnvironment.ContentRootPath, "..", FileHelper.Uploads));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region PostgreSQL Connection
            /*
            var postgreConnectionString = Configuration["PostgreSqlConnectionString"];
            //services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(sqlConnectionString));

            services.AddDbContext<GENERICWriteOnlyCtx>(options =>
               options.UseNpgsql(postgreConnectionString,
                   b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName)
               ), ServiceLifetime.Scoped
            );

            services.AddDbContext<GENERICReadOnlyCtx>(options =>
               options.UseNpgsql(postgreConnectionString,
                   b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName)
               ), ServiceLifetime.Scoped
            );
            */
            #endregion

            #region Entity framework configuration and Identity
            services.AddDbContext<GENERICWriteOnlyCtx>(options =>
               options.UseSqlServer(Configuration.GetConnectionString(DbContextConst.ConnectionString),
                   b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName)
               ), ServiceLifetime.Scoped
            );

            services.AddDbContext<GENERICReadOnlyCtx>(options =>
               options.UseSqlServer(Configuration.GetConnectionString(DbContextConst.ConnectionString),
                   b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName)
            ), ServiceLifetime.Scoped
            );

            services.AddIdentity<User, Role>(options =>
            {
                options.Lockout = new LockoutOptions()
                {
                    MaxFailedAccessAttempts = 20
                };

                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequireNonAlphanumeric = true,
                    RequiredLength = 6,
                    RequiredUniqueChars = 0,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<GENERICWriteOnlyCtx>();
            #endregion
            //In Memory Cache
            services.AddMemoryCache(options =>
            {
                options.CompactionPercentage = 1;
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(Convert.ToDouble(Configuration["OTP:DurationInMinutes"]));
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            //Register All Services
            services.AddScoped(serviceType: typeof(GENERICUnitOfWork), implementationType: typeof(GENERICUnitOfWork));
            services.AddDependencyResolver();
            services.AddSession();
            services.AddHttpClient();

            // Max Body size
            SetupMaxSize(services);

            //Register Automaper
            ConfigureAutoMapper(services);

            // Register the Swagger generator, defining 1 or more Swagger documents
            SetupwaggerGenServices(services);

            //jwt Token
            SetupJWTServices(services);

            

            // Seed data
            //Seed(services);

            // Upload Directory
            if (Directory.Exists(UploadDirectory) == false)
            {
                Directory.CreateDirectory(UploadDirectory);
            }

            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //services.AddMvc().AddNewtonsoftJson(o =>
            //{
            //    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //    o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //    o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            //    // ^^ IMPORTANT PART ^^
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        private void SetupwaggerGenServices(IServiceCollection services)
        {
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TaskSync API",
                    Description = "TaskSync API using .net core"

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    Array.Empty<string>()
                }
                });
                c.SchemaFilter<SwaggerExcludeFilter>();
            });
        }
        private void SetupJWTServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "bearer";
                options.DefaultChallengeScheme = "bearer";
            }).AddJwtBearer("bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                //options.FallbackPolicy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .AddRequirements(new CustomAuthRequirement())
                //    .Build();
            });
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            //MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            //{
            //    config.AddProfile<AutoMapperProfile>();
            //});

            //mapperConfiguration.CreateMapper();

            //services.AddSingleton(mapperConfiguration);
        }

        private void SetupMaxSize(IServiceCollection services)
        {
            long maxBodySize = (1L * 1024L * 1024L * 1024L); // 1GB

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = maxBodySize;//long.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = maxBodySize;  //long.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream",
                FileProvider = new PhysicalFileProvider(UploadDirectory),
                RequestPath = $"/{FileHelper.Uploads}"
            });
            app.UseRouting();
            app.UseSession();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelExpandDepth(2);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskSync API V1");
                c.InjectJavascript("/swagger-custom.js");
                c.EnablePersistAuthorization();
            });

            // global cors policy
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void Seed(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var userManager = serviceProvider.GetService<UserManager<User>>() ?? throw new Exception("Could not load user manager for seeding");
            //var writeOnlyDbContext = serviceProvider.GetRequiredService<GENERICWriteOnlyCtx>();
            var totalUsers = userManager.Users.Count();
            if (totalUsers > 0) return;

            var user = new User { UserName = "admin", Email = "admin@ptsl.com" };
            userManager.CreateAsync(user, "Asd123@").GetAwaiter().GetResult();

        }
    }
}
