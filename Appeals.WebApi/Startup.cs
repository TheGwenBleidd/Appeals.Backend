using Appeals.Application;
using Appeals.Application.Common.Mappings;
using Appeals.Application.Interfaces;
using Appeals.Persistance;
using Appeals.WebApi.Common;
using Appeals.WebApi.Common.Swagger;
using Appeals.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Appeals.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddAutoMapper(options =>
            {
                options.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                options.AddProfile(new AssemblyMappingProfile(typeof(IAppealsDbContext).Assembly));
            });

            services.AddApplication();
            services.AddPersistance(Configuration);
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7133/";
                    options.Audience = "AppealsWebAPI";
                    options.RequireHttpsMetadata = false;
                });

            services.AddMvcCore().AddApiExplorer();
            services.AddApiVersioning(s =>
            {
                // api-deprecated-versions
                // api-supported-versions
                s.ApiVersionReader = new HeaderApiVersionReader("api-version");
                s.AssumeDefaultVersionWhenUnspecified = true;
                s.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            var environment = Configuration["Environment"];
            services.AddSwaggerGen(options =>
            {
                options.DocumentFilter<SwaggerFilterOutControllers>(environment);
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSingleton<ICurrentUserService, ICurrentUserService>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    options.RoutePrefix = string.Empty;
                }
            });
            
            app.UseCustomExceptionHandler();

            app.UseRouting();
            
            app.UseHttpsRedirection();
            
            app.UseCors("AllowAll");
            
            app.UseAuthentication();
            
            app.UseAuthorization();
            
            app.UseApiVersioning();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
