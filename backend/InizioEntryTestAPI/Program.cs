using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Models.Entities;
using Repositories.Implement;
using Repositories.Interface;
using Services.Implement;
using Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    });

    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "InizioEntryTestAPI", Version = "v1" });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllers();
builder.Services.AddScoped<IGoogleScrapeRepository, GoogleScrapeRepository>();
builder.Services.AddScoped<IGoogleScrapeService, GoogleScrapeService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.Configure<SerpApiConfig>(builder.Configuration.GetSection("SerpApiConfig"));

var app = builder.Build();

// Enable Swagger in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
