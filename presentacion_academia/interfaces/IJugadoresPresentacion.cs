using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IJugadoresPresentacion
    {
        Jugadores Insertar(Jugadores entidad);
        Jugadores Actualizar(Jugadores entidad);
        List<Jugadores> Consultar();
        List<Jugadores> Buscar(Jugadores entidad);
        Jugadores Borrar(Jugadores entidad);
    }
}
