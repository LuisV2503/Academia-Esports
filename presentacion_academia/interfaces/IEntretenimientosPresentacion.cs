using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IEntretenimientosPresentacion
    {
        Entretenimientos Insertar(Entretenimientos entidad);
        Entretenimientos Actualizar(Entretenimientos entidad);
        List<Entretenimientos> Consultar();
        List<Entretenimientos> Buscar(Entretenimientos entidad);
        Entretenimientos Borrar(Entretenimientos entidad);
    }
}
