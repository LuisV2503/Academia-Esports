using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEquipos_AcademiasAplicacion
    {
        Equipos_Academias Insertar(Equipos_Academias entidad);
        Equipos_Academias Actualizar(Equipos_Academias entidad);
        List<Equipos_Academias> Consultar();
        Equipos_Academias Borrar(Equipos_Academias entidad);
        List<Equipos_Academias> Buscar(Equipos_Academias entidad);
    }
}
