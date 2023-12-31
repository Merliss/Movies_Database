using System.Text;
using System.Transactions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Movies_Database;
using Movies_Database.Authorization;
using Movies_Database.Entities;
using Movies_Database.Middleware;
using Movies_Database.Models;
using Movies_Database.Models.Validators;
using Movies_Database.Services;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<MovieDbContext>();
builder.Services.AddScoped<MovieSeeder>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMovieService,MovieService>();
builder.Services.AddScoped<IMovieRatingService,MovieRatingService>();
builder.Services.AddScoped<IAccountService,AccountService>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();

builder.Services.AddScoped<IUserContextService,UserContextService>();

builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();

builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<CreateMovieDto>, CreateMovieDtoValidator>();
builder.Services.AddScoped<IValidator<CreateMovieRatingDto>, CreateMovieRatingDtoValidator>();
builder.Services.AddScoped<IValidator<MovieQuery>, MovieQueryValidator>();

builder.Services.AddScoped<IAuthorizationHandler,ResourceOperationsRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler,CreatedMultipleRatingsRequirementHandler>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendClient", builder =>

            builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://exapmle_movie_db_front.com") //change with willing url
    ); 
});
builder.Services.AddAuthorization(
    options => {

        options.AddPolicy("CreatedAtLeast2Ratings", builder => builder.AddRequirements(new CreatedMultipleRatingsRequirement(2)));
        
    });

builder.Host.UseNLog();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))

    };
});


var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<MovieSeeder>();

seeder.Seed();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseAuthentication();
app.UseCors("FrontendClient");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
