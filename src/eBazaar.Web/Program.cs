using eBazaar.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    // Redirect root to Products
    options.Conventions.AddPageRoute("/Products/Index", "");
});

// Register HttpClient
builder.Services.AddHttpClient();

// Register API Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
