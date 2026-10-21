using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IPersonasAplicacion
    {
        Personas Insertar(Personas entidad);
        Personas Actualizar(Personas entidad);
        List<Personas> Consultar();
        Personas Borrar(Personas entidad);
    }
}
