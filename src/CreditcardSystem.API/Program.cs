using System.Text;
using System.Text.Json.Serialization;
using CreditcardSystem.API.Middlewares;
using CreditcardSystem.Infra.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder
    .Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.ConfigureInfra();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Description =
                "Standard authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
    );

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)
            ),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "CreditcardSystemDocumentation");
        x.RoutePrefix = "docs";
    });
}

app.UseHttpsRedirection();

app.UsePathBase("/docs");
app.Use(
    async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/docs/index.html");
        }
        await next();
    }
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();

// app.UseMiddleware<AuthMiddleware>();
app.Run();
