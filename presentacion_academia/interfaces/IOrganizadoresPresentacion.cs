using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IOrganizadoresPresentacion
    {
        Organizadores Insertar(Organizadores entidad);
        Organizadores Actualizar(Organizadores entidad);
        List<Organizadores> Consultar();
        List<Organizadores> Buscar(Organizadores entidad);
        Organizadores Borrar(Organizadores entidad);
    }
}
