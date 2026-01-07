using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projectApiAngular.Configurations;
using projectApiAngular.Data;
using projectApiAngular.Middleware;
using projectApiAngular.Repositories;
using projectApiAngular.Services;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")  // כתובת הלקוח (ה-Frontend)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        "Logs/app-.log",
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

builder.Host.UseSerilog();


// Add services to the container.
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("jwtSettings"));
JwtSettings? JwtSettings = builder.Configuration.GetSection("jwtSettings").Get<JwtSettings>();
if (JwtSettings == null)
{
    throw new InvalidOperationException("JWT settings are not configured properly.");
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IDonnerRepository, DonnerRepository>();
builder.Services.AddScoped<IDonnerService, DonnerService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurcheseServicecs>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>(); 
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddHttpContextAccessor();



builder.Services.AddDbContext<Chinese_SalesDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{ 
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = !string.IsNullOrEmpty(JwtSettings?.Issuer),
        ValidateAudience = !string.IsNullOrEmpty(JwtSettings?.Audience),
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtSettings?.Issuer,
        ValidAudience = JwtSettings?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings!.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("requireAdmin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();
app.UseMiddleware<RequestLog>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost");

app.UseRouting();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
