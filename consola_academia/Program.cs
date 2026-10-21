using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;

using Microsoft.EntityFrameworkCore;


try
{
    IConexion conexion = new Conexion();
    conexion.StringConexion = Dbconexion.StringConexion();


    var lista_sueldos = conexion.Sueldos!.ToList();
    var lista_personas = conexion.Personas!.ToList();
    var lista_equipos = conexion.Equipos!.ToList();
    var lista_juegos = conexion.Juegos!.ToList();
    var lista_tipo = conexion.Tipo!.ToList();
    var lista_academias = conexion.Academias!.ToList();
    var lista_eventos = conexion.Eventos!.ToList();


    var lista_jugadores = conexion.Jugadores!
        .Include(x => x._persona)
        .Include(x => x._equipo)
        .ToList();

    var lista_entrenadores = conexion.Entrenadores!
        .Include(x => x._persona)
        .Include(x => x._equipo)
        .ToList();

    var lista_analistas = conexion.Analistas!
        .Include(x => x._persona)
        .Include(x => x._equipo)
        .ToList();

    var lista_psicologos = conexion.Psicologos!
        .Include(x => x._persona)
        .Include(x => x._equipo)
        .ToList();

    var lista_fisioterapeutas = conexion.Fisioterapeutas!
        .Include(x => x._persona)
        .Include(x => x._equipo)
        .ToList();

    var lista_organizadores = conexion.Organizadores!
        .Include(x => x._persona)
        .ToList();

    var lista_empresas = conexion.Empresas!
        .Include(x => x._tipo)
        .ToList();

    var lista_equipos_juegos = conexion.Equipos_Juegos!
        .Include(x => x._equipos)
        .Include(x => x._juegos)
        .ToList();

    var lista_equipos_empresas = conexion.Equipos_Empresas!
        .Include(x => x._equipos)
        .Include(x => x._empresas)
        .ToList();

    var lista_equipos_academias = conexion.Equipos_Academias!
        .Include(x => x._equipos)
        .Include(x => x._academias)
        .ToList();

    var lista_empresas_academias = conexion.Empresas_Academias!
        .Include(x => x._empresas)
        .Include(x => x._academias)
        .ToList();

    var lista_empresas_academias_eventos = conexion.Empresas_Academias_Eventos!
        .Include(x => x._empresas)
        .Include(x => x._academias)
        .Include(x => x._eventos)
        .ToList();

    var lista_eventos_organizadores = conexion.Eventos_Organizadores!
        .Include(x => x._eventos)
        .Include(x => x._organizadores)
        .ToList();

    var lista_entretenimientos = conexion.Entretenimientos!
        .Include(x => x._eventos)
        .ToList();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("csl_presentacion");

