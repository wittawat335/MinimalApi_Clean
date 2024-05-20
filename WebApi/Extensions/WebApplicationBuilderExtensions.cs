using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using InfrastructureLayer;
using ApplicationLayer;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
        {
            #region Logging

            _ = builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
            {
                var assembly = Assembly.GetEntryAssembly();

                _ = loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                    .Enrich.WithProperty(
                        "Assembly Version",
                        assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version)
                    .Enrich.WithProperty(
                        "Assembly Informational Version",
                        assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
            });

            #endregion Logging

            #region Serialisation

            _ = builder.Services.Configure<JsonOptions>(opt =>
            {
                opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                opt.SerializerOptions.PropertyNameCaseInsensitive = true;
                opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

            #endregion Serialisation

            #region Swagger

            var ti = CultureInfo.CurrentCulture.TextInfo;

            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.DocInclusionPredicate((name, api) => true);
            });

            #endregion Swagger

            #region Validation

            _ = builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);

            #endregion Validation

            #region Project Dependencies

            _ = builder.Services.AddInfrastructure(builder.Configuration);
            _ = builder.Services.AddApplication();

            #endregion Project Dependencies


            return builder;
        }
    }
}
