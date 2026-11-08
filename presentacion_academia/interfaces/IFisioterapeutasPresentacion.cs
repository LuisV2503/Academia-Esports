using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface IFisioterapeutasPresentacion
    {
        Fisioterapeutas Insertar(Fisioterapeutas entidad);
        Fisioterapeutas Actualizar(Fisioterapeutas entidad);
        List<Fisioterapeutas> Consultar();
        List<Fisioterapeutas> Buscar(Fisioterapeutas entidad);
        Fisioterapeutas Borrar(Fisioterapeutas entidad);
    }
}
