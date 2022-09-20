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
builder.Services.AddHttpClient<I195MailUsServ, S195MailUsServ>(client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient <I202ContactoServ, S202ContactoServ> (client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient <I204ContDetServ, S204ContDetServ> (client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient <I205DatosTipoServ, S205DatosTipoServ> (client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient <I800WebSiteServ, S800WebSiteServ> (client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient<I802WebConfigServ, S802WebConfigServ>(client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient<I804WebCapturaServ, S804WebCapturaServ>(client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
builder.Services.AddHttpClient <I840ContactanosServ, S840ContactanosServ> (client =>
{ client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();
