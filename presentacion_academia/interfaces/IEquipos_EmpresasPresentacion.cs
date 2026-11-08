using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEquipos_EmpresasPresentacion
    {
        Equipos_Empresas Insertar(Equipos_Empresas entidad);
        Equipos_Empresas Actualizar(Equipos_Empresas entidad);
        List<Equipos_Empresas> Consultar();
        List<Equipos_Empresas> Buscar(Equipos_Empresas entidad);
        Equipos_Empresas Borrar(Equipos_Empresas entidad);
    }
}
