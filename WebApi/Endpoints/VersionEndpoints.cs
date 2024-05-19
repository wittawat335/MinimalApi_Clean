using MediatR;

namespace WebApi.Endpoints
{
    public static class VersionEndpoints
    {
        public static WebApplication MapVersionEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/api/version")
                .WithTags("version")
                .WithOpenApi();

        
            return app;
        }

       
    }
}
