#region Imports

using System.Text;
using FrontendWebApi.Data;
using FrontEndWebAPI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#endregion

// Configure Entity Framework and SQL Server
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient(); // Injects HttpClient to the Controllers
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5000/") });

#region JWT Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
#endregion

#region Response Caching

builder.Services.AddResponseCaching();

#endregion

#region Controller Configuration (UnhandledExceptionFilter)

builder.Services.AddControllers(options =>
{
    // Log exception and return standard ApiResponse object with error details.
    // NO NEED OF TRY..CATCH blocks in Controllers.
    options.Filters.Add(typeof(UnhandledExceptionFilter));
});

#endregion

#region Swagger/OpenAPI

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

builder.Services.AddCors(policy => {

    policy.AddPolicy("CorsPolicy_Allow_Blazar_App", builder =>
      builder.WithOrigins("https://*:7208/")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyOrigin()
        .AllowAnyHeader()

 );
});

var app = builder.Build();

#region App Runner Configuration

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseResponseCaching();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy_Allow_Blazar_App");

app.MapControllers();

#endregion


app.Run();



