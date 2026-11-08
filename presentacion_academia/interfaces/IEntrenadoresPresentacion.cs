using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEntrenadoresPresentacion
    {
        Entrenadores Insertar(Entrenadores entidad);
        Entrenadores Actualizar(Entrenadores entidad);
        List<Entrenadores> Consultar();
        List<Entrenadores> Buscar(Entrenadores entidad);
        Entrenadores Borrar(Entrenadores entidad);
    }
}
