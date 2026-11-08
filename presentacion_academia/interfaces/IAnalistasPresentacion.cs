using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IAnalistasPresentacion
    {
        Analistas Insertar(Analistas entidad);
        Analistas Actualizar(Analistas entidad);
        List<Analistas> Consultar();
        List<Analistas> Buscar(Analistas entidad);
        Analistas Borrar(Analistas entidad);
    }
}
