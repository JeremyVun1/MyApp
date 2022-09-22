using Microsoft.AspNetCore.HttpOverrides;
using MyApp.Proxies;
using MyApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSystemd();

// Add services to the container.
var configuration = builder.Configuration;

var services = builder.Services;
services.AddHttpClient<ICounterProxy, CounterProxy>(client => {
    client.BaseAddress = new Uri(configuration.GetValue<string>("MyApi:BaseUrl"));
});

services.AddScoped<ICounterService, CounterService>();

services.AddRazorPages();
services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
