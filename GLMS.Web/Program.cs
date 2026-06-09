using GLMS.Web.Services;
using GLMS.API.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// 1. Configure the HttpClient with explicit full namespaces to avoid ambiguity
builder.Services.AddHttpClient<GLMS.Web.Services.IContractService, GLMS.Web.Services.ContractService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7123/");
});


// 2. Add standard MVC Services
builder.Services.AddControllersWithViews();

// 3. Register decoupled local services (No repositories or database contexts allowed here!)
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
