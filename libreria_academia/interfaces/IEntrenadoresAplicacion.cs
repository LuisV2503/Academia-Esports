using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEntrenadoresAplicacion
    {
        Entrenadores Insertar(Entrenadores entidad);
        Entrenadores Actualizar(Entrenadores entidad);
        List<Entrenadores> Consultar();
        Entrenadores Borrar(Entrenadores entidad);
        List<Entrenadores> Buscar(Entrenadores entidad);
    }
}
