using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEventosPresentacion
    {
        Eventos Insertar(Eventos entidad);
        Eventos Actualizar(Eventos entidad);
        List<Eventos> Consultar();
        List<Eventos> Buscar(Eventos entidad);
        Eventos Borrar(Eventos entidad);
    }
}
