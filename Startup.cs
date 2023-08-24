using GraphqlNamespace;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

public class Startup
{
    private const string _defaultCorsPolicyName = "AllowAny";
    private const string _defaultApiVersion = "v1";
    private const string _microServiceApiName = "TestAPI";
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(
            options => options.AddPolicy(
                _defaultCorsPolicyName,
                builder => builder
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
        ));
        services.AddControllers();
        services.AddDistributedMemoryCache();
        services.AddHttpContextAccessor();
        ConfigureSwagger(services);

        var bookData = new List<Book>
            {
                new Book { id = 1, title = "Sun light", author = "abc123" },
                new Book { id = 2, title = "Moon light", author = "xyz456" }
            };
        services.AddSingleton<List<Book>>(bookData);
        services.AddGraphQLServer().AddQueryType<BookQuery>().AddMutationType<BookMutation>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(_defaultCorsPolicyName);
        app.UseSwagger();
        app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{_defaultApiVersion}/swagger.json", $"Testing.{_microServiceApiName} {_defaultApiVersion}");
                c.RoutePrefix = "swagger";

                c.DocumentTitle = $"{_microServiceApiName} - Swagger UI";
                c.DefaultModelsExpandDepth(-1);
                c.ConfigObject.AdditionalItems["authorization"] = new
                {
                    enabled = true,
                    name = "Authorization",
                    value = "Bearer {JWT token}"
                };
                c.OAuthUsePkce();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
            });

        app.UseDeveloperExceptionPage();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
            endpoints.MapGraphQL();
        });
    }
    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(_defaultApiVersion, new OpenApiInfo { Title = $"Testing.Services.{_microServiceApiName}", Version = _defaultApiVersion });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Enter 'Bearer' [space] and your token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                                {
                                    new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type=ReferenceType.SecurityScheme,
                                            Id="Bearer"
                                        },
                                        Scheme="oauth2",
                                        Name="Bearer",
                                        In=ParameterLocation.Header
                                    },
                                    new List<string>()
                                }
            });
        });
    }
}
