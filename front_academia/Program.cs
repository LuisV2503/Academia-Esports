using aplicacion_academia.interfaces;
using presentacion_academia.interfaces;
using presentacion_academia.implementaciones;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IComunicaciones, Comunicaciones>();
builder.Services.AddScoped<ISueldosPresentacion, SueldosPresentacion>();
builder.Services.AddScoped<IPersonasPresentacion, PersonasPresentacion>();
builder.Services.AddScoped<ITipoPresentacion, TipoPresentacion>();
builder.Services.AddScoped<IEquiposPresentacion, EquiposPresentacion>();
builder.Services.AddScoped<IJuegosPresentacion, JuegosPresentacion>();
builder.Services.AddScoped<IAcademiasPresentacion, AcademiasPresentacion>();
builder.Services.AddScoped<IEventosPresentacion, EventosPresentacion>();
builder.Services.AddScoped<IEmpresasPresentacion, EmpresasPresentacion>();
builder.Services.AddScoped<IEntretenimientosPresentacion, EntretenimientosPresentacion>();
builder.Services.AddScoped<IJugadoresPresentacion, JugadoresPresentacion>();
builder.Services.AddScoped<IEntrenadoresPresentacion, EntrenadoresPresentacion>();
builder.Services.AddScoped<IAnalistasPresentacion, AnalistasPresentacion>();
builder.Services.AddScoped<IPsicologosPresentacion, PsicologosPresentacion>();
builder.Services.AddScoped<IFisioterapeutasPresentacion, FisioterapeutasPresentacion>();
builder.Services.AddScoped<IOrganizadoresPresentacion, OrganizadoresPresentacion>();
builder.Services.AddScoped<IEquipos_JuegosPresentacion, Equipos_JuegosPresentacion>();
builder.Services.AddScoped<IEquipos_EmpresasPresentacion, Equipos_EmpresasPresentacion>();
builder.Services.AddScoped<IEquipos_AcademiasPresentacion, Equipos_AcademiasPresentacion>();
builder.Services.AddScoped<IEmpresas_AcademiasPresentacion, Empresas_AcademiasPresentacion>();
builder.Services.AddScoped<IEmpresas_Academias_EventosPresentacion, Empresas_Academias_EventosPresentacion>();
builder.Services.AddScoped<IEventos_OrganizadoresPresentacion, Eventos_OrganizadoresPresentacion>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();