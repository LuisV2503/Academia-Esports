using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEventosAplicacion
    {
        Eventos Insertar(Eventos entidad);
        Eventos Actualizar(Eventos entidad);
        List<Eventos> Consultar();
        Eventos Borrar(Eventos entidad);
        List<Eventos> Buscar(Eventos entidad);
    }
}
