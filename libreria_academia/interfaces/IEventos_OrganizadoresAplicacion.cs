using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEventos_OrganizadoresAplicacion
    {
        Eventos_Organizadores Insertar(Eventos_Organizadores entidad);
        Eventos_Organizadores Actualizar(Eventos_Organizadores entidad);
        List<Eventos_Organizadores> Consultar();
        Eventos_Organizadores Borrar(Eventos_Organizadores entidad);
        List<Eventos_Organizadores> Buscar(Eventos_Organizadores entidad);
    }
}
