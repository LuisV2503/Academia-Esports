using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IAcademiasPresentacion
    {
        Academias Insertar(Academias entidad);
        Academias Actualizar(Academias entidad);
        List<Academias> Consultar();
        List<Academias> Buscar(Academias entidad);
        Academias Borrar(Academias entidad);
    }
}
