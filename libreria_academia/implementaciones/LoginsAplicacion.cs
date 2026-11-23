using libreria_academia.entidades;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class LoginsAplicacion : ILoginsAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public LoginsAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Logins Actualizar(Logins entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Logins!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (entidad.Usuario == null || entidad.Usuario == 0 ||
                string.IsNullOrEmpty(entidad.Imagen))
                throw new Exception("Faltan valores");

            if (!this.conexion.Usuarios!.Any(x => x.Id == entidad.Usuario))
                throw new Exception("Usuario invalido");

            if (this.conexion.Logins!.Any(x => x.Id != entidad.Id && x.Usuario == entidad.Usuario))
                throw new Exception("El usuario ya tiene login");

            if (entidad.Activo == null)
                entidad.Activo = false;

            // Si no se escribe una clave nueva, se conserva la que ya está guardada
            if (string.IsNullOrEmpty(entidad.Contrasena))
            {
                entidad.Contrasena = this.conexion.Logins!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Contrasena)
                    .FirstOrDefault();
            }
            else
                entidad.Contrasena = Seguridad.Hash(entidad.Contrasena);

            var entry = this.conexion!.Entry<Logins>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el login del usuario {entidad.Usuario} (Id {entidad.Id})");

            entidad.Contrasena = null;
            return entidad;
        }

        public Logins Borrar(Logins entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Logins!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Logins!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el login del usuario {anterior.Usuario} (Id {anterior.Id})");

            entidad.Contrasena = null;
            return entidad;
        }

        public List<Logins> Buscar(Logins entidad)
        {
            var nombre = entidad._usuario?.Nombre ?? "";

            var lista = this.conexion.Logins!
                .Include(x => x._usuario)
                .Where(x => x._usuario!.Nombre!.Contains(nombre))
                .ToList();

            foreach (var login in lista)
                login.Contrasena = null;
            return lista;
        }

        public List<Logins> Consultar()
        {
            var lista = this.conexion.Logins!
                .Include(x => x._usuario)
                .ToList();

            // El hash nunca sale del servicio
            foreach (var login in lista)
                login.Contrasena = null;
            return lista;
        }

        public Logins Insertar(Logins entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Usuario == null || entidad.Usuario == 0 ||
                string.IsNullOrEmpty(entidad.Contrasena) ||
                string.IsNullOrEmpty(entidad.Imagen))
                throw new Exception("Faltan valores");

            if (!this.conexion.Usuarios!.Any(x => x.Id == entidad.Usuario))
                throw new Exception("Usuario invalido");

            if (this.conexion.Logins!.Any(x => x.Usuario == entidad.Usuario))
                throw new Exception("El usuario ya tiene login");

            if (entidad.Activo == null)
                entidad.Activo = false;

            entidad.Contrasena = Seguridad.Hash(entidad.Contrasena);

            this.conexion.Logins!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el login del usuario {entidad.Usuario} (Id {entidad.Id})");

            entidad.Contrasena = null;
            return entidad;
        }

        // Valida correo y clave. Devuelve el usuario con su rol (nunca el hash).
        public Usuarios Validar(Credenciales entidad)
        {
            if (string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Contrasena))
                throw new Exception("Credenciales invalidas");

            var usuario = this.conexion.Usuarios!
                .Include(x => x._rol)
                .FirstOrDefault(x => x.Correo == entidad.Correo);
            if (usuario == null)
                throw new Exception("Credenciales invalidas");

            var login = this.conexion.Logins!
                .FirstOrDefault(x => x.Usuario == usuario.Id);
            if (login == null ||
                !string.Equals(login.Contrasena, Seguridad.Hash(entidad.Contrasena),
                    StringComparison.OrdinalIgnoreCase))
                throw new Exception("Credenciales invalidas");

            if (usuario.Activo != true || login.Activo != true)
                throw new Exception("Cuenta desactivada");

            this.auditoria.Registrar("inició sesión", usuario.Id);
            return usuario;
        }

        // Para comprobar en cada página que la cuenta sigue habilitada y con qué rol
        public Usuarios Estado(Usuarios entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("Cuenta desactivada");

            var usuario = this.conexion.Usuarios!
                .Include(x => x._rol)
                .FirstOrDefault(x => x.Id == entidad.Id);
            var login = this.conexion.Logins!
                .FirstOrDefault(x => x.Usuario == entidad.Id);

            if (usuario == null || login == null ||
                usuario.Activo != true || login.Activo != true)
                throw new Exception("Cuenta desactivada");

            return usuario;
        }
    }
}
