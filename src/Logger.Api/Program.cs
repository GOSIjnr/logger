using System.Text.Json;
using System.Text.Json.Serialization;
using Logger.Api.Authentication;
using Logger.Api.Constants.Auth;
using Logger.Api.Endpoints.Auth;
using Logger.Api.Endpoints.Base;
using Logger.Api.Endpoints.Incidents;
using Logger.Api.Endpoints.Organizations;
using Logger.Api.Endpoints.Sheets;
using Logger.Api.Endpoints.Users;
using Logger.Api.Extensions.Claims;
using Logger.Api.Extensions.OpenApi;
using Logger.Api.Middleware;
using Logger.Application;
using Logger.Application.CQRS.Messaging;
using Logger.Infrastructure;
using Logger.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration, [typeof(IMediator).Assembly]);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(AuthenticationSchemes.Session)
    .AddScheme<AuthenticationSchemeOptions, SessionAuthenticationHandler>(
        AuthenticationSchemes.Session,
        options => { options.ClaimsIssuer = "Logger"; }
    );

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthenticationResultHandler>();

builder.Services.AddAuthorizationBuilder().AddCustomPolicies();

builder.Services.Configure<JsonOptions>(opts =>
{
    JsonSerializerOptions serializer = opts.SerializerOptions;

    serializer.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    serializer.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    serializer.WriteIndented = true;
    serializer.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddOpenApi(options => { options.AddCustomOpenApiTransformer(); });

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.Title = "Logger API Gateway";
        options.DefaultHttpClient = new(ScalarTarget.Node, ScalarClient.Fetch);
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<TraceIdMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<SessionRefreshMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapBaseEndpoints();
app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapOrganizationEndpoints();
app.MapSheetEndpoints();
app.MapIncidentEndpoints();

app.Run();
