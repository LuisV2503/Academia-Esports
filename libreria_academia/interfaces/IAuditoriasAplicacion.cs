using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IAuditoriasAplicacion
    {
        // Escribe "El usuario {nombre} {accion}". Si no se indica el usuario, se toma el de la petición.
        void Registrar(string accion, int? usuario = null);
        List<Auditorias> Consultar();
        List<Auditorias> Buscar(Auditorias entidad);
    }
}
