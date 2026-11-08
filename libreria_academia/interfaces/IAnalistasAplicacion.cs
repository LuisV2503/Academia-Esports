using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IAnalistasAplicacion
    {
        Analistas Insertar(Analistas entidad);
        Analistas Actualizar(Analistas entidad);
        List<Analistas> Consultar();
        Analistas Borrar(Analistas entidad);
        List<Analistas> Buscar(Analistas entidad);
    }
}
