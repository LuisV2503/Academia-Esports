using libreria_academia.entidades;

namespace presentacion_academia.interfaces
{
    public interface ILoginsPresentacion
    {
        Logins Insertar(Logins entidad);
        Logins Actualizar(Logins entidad);
        List<Logins> Consultar();
        List<Logins> Buscar(Logins entidad);
        Logins Borrar(Logins entidad);
        Usuarios Validar(Credenciales entidad);
        Usuarios Estado(Usuarios entidad);
    }
}
