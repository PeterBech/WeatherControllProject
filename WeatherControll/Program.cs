var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

//var builder = WebApplication.CreateBuilder(args);

//// Tilføj konfiguration fra appsettings.json (hvis ikke allerede tilføjet)
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//// Registrer HttpClient til dependency injection
//builder.Services.AddHttpClient();

//// Tilføj controllers med views
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Konfigurer middleware-pipeline
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseAuthorization();

//// Konfigurer routes
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Weather}/{action=Index}/{id?}");

//app.Run();

