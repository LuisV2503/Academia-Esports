using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IPsicologosPresentacion
    {
        Psicologos Insertar(Psicologos entidad);
        Psicologos Actualizar(Psicologos entidad);
        List<Psicologos> Consultar();
        List<Psicologos> Buscar(Psicologos entidad);
        Psicologos Borrar(Psicologos entidad);
    }
}
