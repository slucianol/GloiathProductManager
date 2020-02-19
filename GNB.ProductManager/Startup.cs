using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GNB.Core.Interfaces;
using GNB.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using GNB.ProductManager.Helpers;

namespace GNB.ProductManager {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(configureOptions => {
                        configureOptions.TokenValidationParameters = new TokenValidationParameters() {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "Gloiath",
                            ValidAudience = "Gloiath",
                            IssuerSigningKey = JwtSecurityKeyGenerator.Get(Configuration.GetValue<string>("Token:Secret"))
                        };
                    });
            services.AddSwaggerGen(setupAction => {
                setupAction.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Version = "v1",
                    Title = "Transactions Api"
                    //Contact = new Microsoft.OpenApi.Models.OpenApiContact {
                    //    Name = "Samuel Luciano Lassis",
                    //    Email = "sa.lassis@gmail.com",
                    //    Url = new Uri("https://linkedin.com/in/samuelluciano/")
                    //}
                });
                setupAction.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Description = "Token de seguridad para validación de usuarios.",
                    Name = "www-authenticate",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });
            });
            services.AddSingleton<IRateCacheServiceUri>(new RateCacheServiceUri(Configuration.GetValue<string>("CacheStoreFilePath")));
            services.AddSingleton<IRateServiceUri>(new RateServiceUri(Configuration.GetValue<string>("ServicesUri:RatesServiceUri")));
            services.AddSingleton<ITransactionServiceUri>(new TransactionServiceUri(Configuration.GetValue<string>("ServicesUri:TransactionServiceUri")));
            services.AddTransient<IRateCacheService, RateCacheService>();
            services.AddSingleton<ILoggerService>(new LoggerService("Logs.json"));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IRatesService, RatesService>();
            services.AddTransient<IRateConverterService, RateConverterService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "Transactions Api");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
