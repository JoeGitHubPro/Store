
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Identity.Confg;
using Store.Core.Identity.Models;
using Store.Core.Identity.Services;
using System.Text;
using System;
using Store.EF.Data;
using Store.Core.UnitWork;
using Store.EF.UnitWork;
using Store.Identity.Services;
using Store.Core.Const;

namespace Store
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //enable file uploads
            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue; // Set maximum request body size if needed
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));



            //SqlServer provider & DefaultConnection
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Repository pattern settings

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                   p => p.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
             );

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


            #region Identity
            // Put it before var app = builder.Build();

            // Add JWT service
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            // Map ApplicationUser 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            // Map service for auth service
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Add default schema config
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });



            #endregion


            var app = builder.Build();

            builder.Services.AddCors();
            app.UseCors(a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Create database and create roles
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                context.Database.MigrateAsync().Wait();

                DefulteRoles.InitializeRoles(context).Wait();
            }


            // Add Authentication
            app.UseAuthentication(); // Put it before app.UseAuthorization() and after var app = builder.Build();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
