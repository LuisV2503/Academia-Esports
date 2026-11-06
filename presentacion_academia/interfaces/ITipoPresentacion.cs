using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface ITipoPresentacion
    {
        Tipo Insertar(Tipo entidad);
        Tipo Actualizar(Tipo entidad);
        List<Tipo> Consultar();
        List<Tipo> Buscar(Tipo entidad);
        Tipo Borrar(Tipo entidad);
    }
}
