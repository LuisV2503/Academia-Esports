using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface ILoginsAplicacion
    {
        Logins Insertar(Logins entidad);
        Logins Actualizar(Logins entidad);
        List<Logins> Consultar();
        Logins Borrar(Logins entidad);
        List<Logins> Buscar(Logins entidad);
        Usuarios Validar(Credenciales entidad);
        Usuarios Estado(Usuarios entidad);
    }
}
