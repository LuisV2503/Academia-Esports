using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEventos_OrganizadoresPresentacion
    {
        Eventos_Organizadores Insertar(Eventos_Organizadores entidad);
        Eventos_Organizadores Actualizar(Eventos_Organizadores entidad);
        List<Eventos_Organizadores> Consultar();
        List<Eventos_Organizadores> Buscar(Eventos_Organizadores entidad);
        Eventos_Organizadores Borrar(Eventos_Organizadores entidad);
    }
}
