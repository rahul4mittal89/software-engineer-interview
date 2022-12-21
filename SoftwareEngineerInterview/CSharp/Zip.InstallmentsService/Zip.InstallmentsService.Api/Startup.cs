using AutoMapper;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Zip.InstallmentsService.Api.Bootstrapper;
using Zip.InstallmentsService.Api.Common.Mappers;

namespace Zip.InstallmentsService.Api;

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
        services.ConfigureUnitOfWork();
        services.ConfigureMediator();
        services.AddAutoMapper(typeof(PaymentPlanMapper));
        services.AddEntityFrameworkConfiguration(
            Configuration.GetConnectionString("PaymentPlanServiceInMemoryDatabase"));
        services.AddRepositories();
        services.AddControllers();
        services.AddMvcCore()
            .AddNewtonsoftJson()
            .AddApiExplorer()
            .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<Startup>(); });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("Version"),
                new MediaTypeApiVersionReader("Version"));
        });
        services.AddVersionedApiExplorer(options =>
        {
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddProblemDetails(opts =>
        {
            // Control when an exception is included
            opts.IncludeExceptionDetails = (ctx, _) =>
            {
                // Fetch services from HttpContext.RequestServices
                var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Payment Installment Plan Service",
                Version = "v1",
                Description = "Api for the Installment Payment Plan",
                Contact = new OpenApiContact
                {
                    Name = "Rahul", Email = "rahul4mittal89@gmail.com"
                }
            });

            options.DescribeAllParametersInCamelCase();
            services.AddEndpointsApiExplorer();
            options.EnableAnnotations();
            options.CustomSchemaIds(a => a.FullName);
            options.ResolveConflictingActions(a => a.First());
        });


        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                       ForwardedHeaders.XForwardedProto;
            // Only loopback proxies are allowed by default.
            // Clear that restriction because forwarders are enabled by explicit 
            // configuration.
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseForwardedHeaders();
        app.UseProblemDetails();
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Unexpected Error");
                });
            });

        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                swagger.Servers = new List<OpenApiServer>
                    { new() { Url = $"{httpReq.Scheme}://{httpReq.Host}" } };
            });
        });

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Installment Plan Service");
            c.OAuthClientId("swaggerui");
            c.OAuthAppName("Swagger UI");
            c.DefaultModelExpandDepth(-1);
            c.DocExpansion(DocExpansion.None);
        });

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}