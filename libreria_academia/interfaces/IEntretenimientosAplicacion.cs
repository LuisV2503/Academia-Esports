using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEntretenimientosAplicacion
    {
        Entretenimientos Insertar(Entretenimientos entidad);
        Entretenimientos Actualizar(Entretenimientos entidad);
        List<Entretenimientos> Consultar();
        Entretenimientos Borrar(Entretenimientos entidad);
        List<Entretenimientos> Buscar(Entretenimientos entidad);
    }
}
