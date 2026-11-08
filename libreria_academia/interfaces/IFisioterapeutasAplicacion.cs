using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IFisioterapeutasAplicacion
    {
        Fisioterapeutas Insertar(Fisioterapeutas entidad);
        Fisioterapeutas Actualizar(Fisioterapeutas entidad);
        List<Fisioterapeutas> Consultar();
        Fisioterapeutas Borrar(Fisioterapeutas entidad);
        List<Fisioterapeutas> Buscar(Fisioterapeutas entidad);
    }
}
