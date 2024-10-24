using ChurchStore.Web;
using ChurchStore.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

//Add cookie configurarion
builder.Services.AddAuthentication("CookieAuthentication")
               .AddCookie("CookieAuthentication", config =>
               {
                   config.Cookie.Name = "FUID";
                   config.LoginPath = "/Login/Index"; //Autorize
                   config.AccessDeniedPath = "/Login/AcessoNegado"; //Role
               });

// Registrar IApiService
builder.Services.AddTransient<IApiService, ApiService>();

var app = builder.Build();

//Starts Configuration Helper to use a static class
ConfigurationHelper.Initialize(builder.Configuration);

//Starts HttpContextAccessor Helper to use a static class
//https://www.appsloveworld.com/csharp/100/4/access-the-current-httpcontext-in-asp-net-core
HttpContextAccessorHelper.Initialize(app.Services.GetRequiredService<IHttpContextAccessor>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
