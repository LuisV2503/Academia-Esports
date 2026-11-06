using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEmpresasPresentacion
    {
        Empresas Insertar(Empresas entidad);
        Empresas Actualizar(Empresas entidad);
        List<Empresas> Consultar();
        List<Empresas> Buscar(Empresas entidad);
        Empresas Borrar(Empresas entidad);
    }
}
