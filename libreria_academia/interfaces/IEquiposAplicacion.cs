using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEquiposAplicacion
    {
        Equipos Insertar(Equipos entidad);
        Equipos Actualizar(Equipos entidad);
        List<Equipos> Consultar();
        Equipos Borrar(Equipos entidad);
        List<Equipos> Buscar(Equipos entidad);
    }
}
