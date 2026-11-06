using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IAcademiasAplicacion
    {
        Academias Insertar(Academias entidad);
        Academias Actualizar(Academias entidad);
        List<Academias> Consultar();
        Academias Borrar(Academias entidad);
        List<Academias> Buscar(Academias entidad);
    }
}
