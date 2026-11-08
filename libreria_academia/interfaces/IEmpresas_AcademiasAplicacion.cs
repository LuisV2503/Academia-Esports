using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEmpresas_AcademiasAplicacion
    {
        Empresas_Academias Insertar(Empresas_Academias entidad);
        Empresas_Academias Actualizar(Empresas_Academias entidad);
        List<Empresas_Academias> Consultar();
        Empresas_Academias Borrar(Empresas_Academias entidad);
        List<Empresas_Academias> Buscar(Empresas_Academias entidad);
    }
}
