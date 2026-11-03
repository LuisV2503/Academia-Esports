using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface ISueldosPresentacion
    {
        Sueldos Insertar(Sueldos entidad);
        Sueldos Actualizar(Sueldos entidad);
        List<Sueldos> Consultar();
        List<Sueldos> Buscar(Sueldos entidad);
        Sueldos Borrar(Sueldos entidad);
    }
}
