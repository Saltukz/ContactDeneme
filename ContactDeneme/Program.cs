using ContactDeneme.Business.Abstract;
using ContactDeneme.Business.Concrete;
using ContactDeneme.Data.Abstract;
using ContactDeneme.Data.Concrete;
using ContactDeneme.Data.Concrete.EfCore;
using ContactDeneme.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

builder.Services.AddDbContext<ContactContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});

builder.Services.AddStackExchangeRedisCache(action =>
{
    action.Configuration = "127.0.0.1:6379";
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
 {
     options.Password.RequiredLength = 6;
     options.Password.RequireNonAlphanumeric = false;
     options.Password.RequireDigit = false;
     options.Password.RequiredUniqueChars = 0;
     options.Password.RequireUppercase = false;
     options.Password.RequireLowercase = false;
     options.User.RequireUniqueEmail = false;
     options.Lockout.MaxFailedAccessAttempts = 5;
     options.Lockout.AllowedForNewUsers = false;
     options.SignIn.RequireConfirmedEmail = true;
     options.User.AllowedUserNameCharacters = "abcçdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+/";



 }).AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();

builder.Services.AddSession();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "member_cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "saltukz.com",
            ValidAudience = "saltukz.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("00584011534043FF979EA331ABB70B6D"))

        };
    });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactInfoService, ContactInfoManager>();
builder.Services.AddScoped<IRegionService, RegionManager>();

builder.Services.AddControllers();

builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
