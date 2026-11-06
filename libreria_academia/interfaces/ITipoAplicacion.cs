using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface ITipoAplicacion
    {
        Tipo Insertar(Tipo entidad);
        Tipo Actualizar(Tipo entidad);
        List<Tipo> Consultar();
        Tipo Borrar(Tipo entidad);
        List<Tipo> Buscar(Tipo entidad);
    }
}
