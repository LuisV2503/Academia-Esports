using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IAuditoriasPresentacion
    {
        List<Auditorias> Consultar();
        List<Auditorias> Buscar(Auditorias entidad);
    }
}
