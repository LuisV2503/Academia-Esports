using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEquipos_EmpresasAplicacion
    {
        Equipos_Empresas Insertar(Equipos_Empresas entidad);
        Equipos_Empresas Actualizar(Equipos_Empresas entidad);
        List<Equipos_Empresas> Consultar();
        Equipos_Empresas Borrar(Equipos_Empresas entidad);
        List<Equipos_Empresas> Buscar(Equipos_Empresas entidad);
    }
}
