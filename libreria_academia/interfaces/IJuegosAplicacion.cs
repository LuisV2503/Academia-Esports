using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IJuegosAplicacion
    {
        Juegos Insertar(Juegos entidad);
        Juegos Actualizar(Juegos entidad);
        List<Juegos> Consultar();
        Juegos Borrar(Juegos entidad);
        List<Juegos> Buscar(Juegos entidad);
    }
}
