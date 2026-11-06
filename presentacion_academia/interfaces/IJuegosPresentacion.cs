using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IJuegosPresentacion
    {
        Juegos Insertar(Juegos entidad);
        Juegos Actualizar(Juegos entidad);
        List<Juegos> Consultar();
        List<Juegos> Buscar(Juegos entidad);
        Juegos Borrar(Juegos entidad);
    }
}
