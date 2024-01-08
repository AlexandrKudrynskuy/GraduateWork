using DLL.Context;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;
using System.Text;
using System.Text.Json.Serialization;
using DLL.Repository;
using BLL.Services;

namespace TradeAssistant.Infrastructure
{
    public class ConfigurationApp
    {
        private WebApplicationBuilder builder;

        public ConfigurationApp(WebApplicationBuilder builder)
        {
            this.builder = builder;
        }

        public void ConfigureService()
        {
            try
            {
                builder.Services.AddControllersWithViews();
                builder.Services.AddTransient<TradeAssistantContext>();
                builder.Services.AddTransient<TestRepository>();
                builder.Services.AddTransient<TestService>();

                builder.Services.AddDbContext<TradeAssistantContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStore")));
                builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<TradeAssistantContext>().AddDefaultTokenProviders();
                builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                    options.JsonSerializerOptions.WriteIndented = true;
                });
                builder.Services.Configure<IdentityOptions>(options =>
                {
                    // Default Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                    // options.Password.RequiredUniqueChars = 1;
                });
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {

                            //you can configure your custom policy
                            builder.AllowAnyOrigin()
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                        });
                });
                builder.Services.AddAuthentication(op =>
                {
                    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }
                ).AddJwtBearer(op =>
                {
                    op.SaveToken = true;
                    op.RequireHttpsMetadata = false;
                    op.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuier"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))


                    };
                });

                builder.Services.AddApplicationInsightsTelemetry();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
