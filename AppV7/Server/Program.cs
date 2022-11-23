using AppV7.Server.Data;
using AppV7.Server.Models;
using AppV7.Server.Models.IFace;
using AppV7.Server.Models.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 4;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    }).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddScoped<IRegistroInicial, RRegistroInicial>();
builder.Services.AddScoped<IAddUsuario, RAddUsuario>();
builder.Services.AddScoped<IEnviarMails, REnviarMails>();
builder.Services.AddScoped<I100Org, R100Org>();
builder.Services.AddScoped<I110Usuarios, R110Usuarios>();
builder.Services.AddScoped<I190Bitacora, R190Bitacora>();
builder.Services.AddScoped<I195MailUs, R195MailUs>();
builder.Services.AddScoped<I202Contacto, R202Contacto>();
builder.Services.AddScoped<I204ContDet, R204ContDet>();
builder.Services.AddScoped<I205DatosTipo, R205DatosTipo>();
builder.Services.AddScoped<I210Almacen, R210Almacen>();
builder.Services.AddScoped<I220Producto, R220Producto>();
builder.Services.AddScoped<I230Solicitud, R230Solicitud>();
builder.Services.AddScoped<I232DetSol, R232DetSol>();
builder.Services.AddScoped<I800WebSite, R800WebSite>();
builder.Services.AddScoped<I810General , R810General>();
builder.Services.AddScoped<I812Files, R812Files>();
builder.Services.AddScoped<I840Contactanos, R840Contactanos>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
