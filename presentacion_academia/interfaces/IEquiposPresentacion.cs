using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEquiposPresentacion
    {
        Equipos Insertar(Equipos entidad);
        Equipos Actualizar(Equipos entidad);
        List<Equipos> Consultar();
        List<Equipos> Buscar(Equipos entidad);
        Equipos Borrar(Equipos entidad);
    }
}
