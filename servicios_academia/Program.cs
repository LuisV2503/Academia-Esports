using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<IConexion>(provider =>
{
    var conexion = new Conexion();
    conexion.StringConexion = Dbconexion.StringConexion();
    return conexion;
});
builder.Services.AddScoped<IPersonasAplicacion, PersonasAplicacion>();
builder.Services.AddScoped<ISueldosAplicacion, SueldosAplicacion>();
builder.Services.AddScoped<ITipoAplicacion, TipoAplicacion>();
builder.Services.AddScoped<IEquiposAplicacion, EquiposAplicacion>();
builder.Services.AddScoped<IJuegosAplicacion, JuegosAplicacion>();
builder.Services.AddScoped<IAcademiasAplicacion, AcademiasAplicacion>();
builder.Services.AddScoped<IEventosAplicacion, EventosAplicacion>();
builder.Services.AddScoped<IEmpresasAplicacion, EmpresasAplicacion>();
builder.Services.AddScoped<IEntretenimientosAplicacion, EntretenimientosAplicacion>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();