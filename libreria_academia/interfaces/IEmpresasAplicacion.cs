using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEmpresasAplicacion
    {
        Empresas Insertar(Empresas entidad);
        Empresas Actualizar(Empresas entidad);
        List<Empresas> Consultar();
        Empresas Borrar(Empresas entidad);
        List<Empresas> Buscar(Empresas entidad);
    }
}
