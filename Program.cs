using GBC_Travel_Group83.Data;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group83.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Custom Error Page
    app.UseStatusCodePagesWithRedirects("/Home/NotFound?statusCode={0}");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();