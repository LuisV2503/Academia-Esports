using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddScoped<IJugadoresAplicacion, JugadoresAplicacion>();
builder.Services.AddScoped<IEntrenadoresAplicacion, EntrenadoresAplicacion>();
builder.Services.AddScoped<IAnalistasAplicacion, AnalistasAplicacion>();
builder.Services.AddScoped<IPsicologosAplicacion, PsicologosAplicacion>();
builder.Services.AddScoped<IFisioterapeutasAplicacion, FisioterapeutasAplicacion>();
builder.Services.AddScoped<IOrganizadoresAplicacion, OrganizadoresAplicacion>();
builder.Services.AddScoped<IEquipos_JuegosAplicacion, Equipos_JuegosAplicacion>();
builder.Services.AddScoped<IEquipos_EmpresasAplicacion, Equipos_EmpresasAplicacion>();
builder.Services.AddScoped<IEquipos_AcademiasAplicacion, Equipos_AcademiasAplicacion>();
builder.Services.AddScoped<IEmpresas_AcademiasAplicacion, Empresas_AcademiasAplicacion>();
builder.Services.AddScoped<IEmpresas_Academias_EventosAplicacion, Empresas_Academias_EventosAplicacion>();
builder.Services.AddScoped<IEventos_OrganizadoresAplicacion, Eventos_OrganizadoresAplicacion>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "text/plain; charset=utf-8";

        var mensaje = ex is DbUpdateException
            ? "Error al guardar en la base de datos"
            : ex?.Message ?? "Error";

        await context.Response.WriteAsync(mensaje);
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();