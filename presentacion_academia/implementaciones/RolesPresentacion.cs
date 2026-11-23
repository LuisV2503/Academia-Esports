using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class RolesPresentacion : IRolesPresentacion
    {
        private IComunicaciones IComunicaciones;

        public RolesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Roles Actualizar(Roles entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Roles>(
                "Roles/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Roles>(
                "Roles/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Roles> Buscar(Roles entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Roles>>(
                "Roles/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Roles> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Roles>>(
                "Roles/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Roles Insertar(Roles entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (string.IsNullOrEmpty(entidad.Imagen))
                throw new Exception("Imagen requerida");

            var task = this.IComunicaciones.Ejecutar<Roles>(
                "Roles/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Roles entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}
