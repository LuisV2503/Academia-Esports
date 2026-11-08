using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IJugadoresAplicacion
    {
        Jugadores Insertar(Jugadores entidad);
        Jugadores Actualizar(Jugadores entidad);
        List<Jugadores> Consultar();
        Jugadores Borrar(Jugadores entidad);
        List<Jugadores> Buscar(Jugadores entidad);
    }
}
