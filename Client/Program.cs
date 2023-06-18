using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Aridio_Rent_A_Car.Client;
using Aridio_Rent_A_Car.Client.Managers;



using Microsoft.AspNetCore.Components.Authorization;
using Aridio_Rent_A_Car.Client.Extensiones;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<IClienteManager, ClienteManager>();
builder.Services.AddScoped<IReservaManager, ReservaManager>();
builder.Services.AddScoped<IUsuarioManager, UsuarioManager>();
builder.Services.AddScoped<IUsuarioRolManager, UsuarioRolManager>();
builder.Services.AddScoped<IVehiculoManager, VehiculoManager>();
builder.Services.AddScoped<ReservaManager>();


builder.Services.AddScoped<HttpClient>(s =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
    // Configura HttpClient según tus necesidades
    // httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer your_token");
    return httpClient;
});

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider,AutenticacionExtension>();
builder.Services.AddAuthorizationCore();

            builder.Services
                .AddBlazorise( options =>
                {
                    options.Immediate = true;
                } )
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();

await builder.Build().RunAsync();
