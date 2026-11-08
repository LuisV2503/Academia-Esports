using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEmpresas_Academias_EventosAplicacion
    {
        Empresas_Academias_Eventos Insertar(Empresas_Academias_Eventos entidad);
        Empresas_Academias_Eventos Actualizar(Empresas_Academias_Eventos entidad);
        List<Empresas_Academias_Eventos> Consultar();
        Empresas_Academias_Eventos Borrar(Empresas_Academias_Eventos entidad);
        List<Empresas_Academias_Eventos> Buscar(Empresas_Academias_Eventos entidad);
    }
}
