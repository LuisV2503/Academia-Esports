using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IPsicologosAplicacion
    {
        Psicologos Insertar(Psicologos entidad);
        Psicologos Actualizar(Psicologos entidad);
        List<Psicologos> Consultar();
        Psicologos Borrar(Psicologos entidad);
        List<Psicologos> Buscar(Psicologos entidad);
    }
}
