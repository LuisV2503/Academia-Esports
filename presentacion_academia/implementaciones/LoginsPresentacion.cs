using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class LoginsPresentacion : ILoginsPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IUsuariosPresentacion IUsuariosPresentacion;

        public LoginsPresentacion(IComunicaciones IComunicaciones,
            IUsuariosPresentacion IUsuariosPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IUsuariosPresentacion = IUsuariosPresentacion;
        }

        public Logins Actualizar(Logins entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            // Al editar, la clave puede ir vacía: entonces se conserva la anterior
            if (Validar(entidad, false))
                throw new Exception("Falta informacion");

            if (!this.IUsuariosPresentacion.Consultar().Any(x => x.Id == entidad.Usuario))
                throw new Exception("Usuario invalido");

            var task = this.IComunicaciones.Ejecutar<Logins>(
                "Logins/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Logins Borrar(Logins entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Logins>(
                "Logins/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Logins> Buscar(Logins entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Logins>>(
                "Logins/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Logins> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Logins>>(
                "Logins/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Logins Insertar(Logins entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad, true))
                throw new Exception("Falta informacion");

            if (!this.IUsuariosPresentacion.Consultar().Any(x => x.Id == entidad.Usuario))
                throw new Exception("Usuario invalido");

            var task = this.IComunicaciones.Ejecutar<Logins>(
                "Logins/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public Usuarios Validar(Credenciales entidad)
        {
            if (string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Contrasena))
                throw new Exception("Credenciales invalidas");

            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Logins/Validar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public Usuarios Estado(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Logins/Estado",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        // nuevo = true: la clave es obligatoria (al crear el login)
        private bool Validar(Logins entidad, bool nuevo)
        {
            return
                entidad.Usuario == null || entidad.Usuario == 0 ||
                (nuevo && string.IsNullOrEmpty(entidad.Contrasena));
        }
    }
}
