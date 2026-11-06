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