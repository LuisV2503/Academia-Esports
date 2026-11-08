using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEquipos_AcademiasPresentacion
    {
        Equipos_Academias Insertar(Equipos_Academias entidad);
        Equipos_Academias Actualizar(Equipos_Academias entidad);
        List<Equipos_Academias> Consultar();
        List<Equipos_Academias> Buscar(Equipos_Academias entidad);
        Equipos_Academias Borrar(Equipos_Academias entidad);
    }
}
