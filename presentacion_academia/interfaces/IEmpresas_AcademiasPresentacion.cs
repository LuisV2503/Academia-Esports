using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEmpresas_AcademiasPresentacion
    {
        Empresas_Academias Insertar(Empresas_Academias entidad);
        Empresas_Academias Actualizar(Empresas_Academias entidad);
        List<Empresas_Academias> Consultar();
        List<Empresas_Academias> Buscar(Empresas_Academias entidad);
        Empresas_Academias Borrar(Empresas_Academias entidad);
    }
}
