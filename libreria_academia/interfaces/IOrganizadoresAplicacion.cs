using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IOrganizadoresAplicacion
    {
        Organizadores Insertar(Organizadores entidad);
        Organizadores Actualizar(Organizadores entidad);
        List<Organizadores> Consultar();
        Organizadores Borrar(Organizadores entidad);
        List<Organizadores> Buscar(Organizadores entidad);
    }
}
