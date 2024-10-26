using Api.Authentication;
using Api.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

var storageType = configuration.GetValue<string>("storageType");
if (storageType == "InMemory")
    builder.Services.AddInMemoryContext();
else if (storageType == "Database")
    builder.Services.AddDatabaseContext(configuration);

builder.Services.AddLogicServices();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie()
    .AddJwtBearer(options =>
    {
        var authOptions = configuration.GetSection("Auth").Get<AuthOptions>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authOptions.ISSUER,
            ValidAudience = authOptions.AUDIENCE,
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey()
        };
    });
builder.Services.AddAuthorization();

var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();
builder.Services.AddSingleton(authOptions);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseSession();
app.Use(async (context, next) =>
{
    var JWToken = context.Session.GetString("JWToken");
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Append("Authorization", "Bearer " + JWToken);
    }
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
