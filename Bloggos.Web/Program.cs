using Bloggos.BussinessLogic.IServices;
using Bloggos.BussinessLogic.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(4); // session duration
    options.Cookie.HttpOnly = true; // security for cookie
    options.Cookie.IsEssential = true; // GDPR/CCPA
});

// Register DbContext
builder.Services.AddDbContext<Bloggos.Database.BloggosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("DatabaseConnectionStrings")["DefaultConnection"]));

// Add Services
builder.Services.AddScoped<IBlogService, MockBlogService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
