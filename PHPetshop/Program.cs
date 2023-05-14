using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PHPetshop.Services.Extensions;
using PHPetshop.Services.Mail;
using PHPetshop.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.RegisterSqlServer(
    builder.Configuration.GetConnectionString("Connection"));
builder.Services.AddScoped<PasswordHasher<Usuario>>();

builder.Services.AddSingleton(options =>
    new MailClient(builder.Configuration.GetMailConfiguration("MailConfiguration")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.Name = "ASPAUTHSESSION";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Home/Forbidden";
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("usuario", policy =>  policy.RequireRole(Cargo.User.ToString()));
    options.AddPolicy("admin", policy => policy.RequireRole(Cargo.Admin.ToString()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
