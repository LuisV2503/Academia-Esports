using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IConexion>(provider =>
{
    var conexion = new Conexion();
    conexion.StringConexion = Dbconexion.StringConexion();
    return conexion;
});
builder.Services.AddScoped<IPersonasAplicacion, PersonasAplicacion>();
builder.Services.AddScoped<ISueldosAplicacion, SueldosAplicacion>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();