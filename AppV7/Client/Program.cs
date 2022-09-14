using AppV7.Client;
using AppV7.Client.Servicios.IFaceServ;
using AppV7.Client.Servicios.Serv;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("AppV7.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AppV7.ServerAPI"));

builder.Services.AddApiAuthorization();

builder.Services.AddHttpClient<IRegistroInicialServ, SRegistroInicialServ>(client => 
{ client.BaseAddress = new(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient<IAddUsuarioServ, SAddUsuarioServ>(client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

builder.Services.AddHttpClient<I100OrgServ, S100OrgServ>(client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient<I110UsuariosServ, S110UsuariosServ>(client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient<I190BitacoraServ, S190BitacoraServ>(client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();
