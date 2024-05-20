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

            _ = root.MapGet("/", GetVersion);
        
            return app;
        }

        public static async Task<IResult> GetVersion(IMediator mediator)
        {
            try
            {
                return Results.Ok("ok");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

    }
}
