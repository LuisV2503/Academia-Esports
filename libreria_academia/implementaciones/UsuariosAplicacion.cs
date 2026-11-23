using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion conexion;

        public UsuariosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Roles!.Any(x => x.Id == entidad.Rol))
                throw new Exception("Rol invalido");

            if (this.conexion.Usuarios!.Any(x => x.Id != entidad.Id && x.Correo == entidad.Correo))
                throw new Exception("Correo repetido");

            if (entidad.Activo == null)
                entidad.Activo = false;

            var entry = this.conexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Logins!.Any(x => x.Usuario == entidad.Id))
                throw new Exception("Usuario en uso");

            this.conexion.Usuarios!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Usuarios> Buscar(Usuarios entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Usuarios!
                .Include(x => x._rol)
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Usuarios> Consultar()
        {
            return this.conexion.Usuarios!
                .Include(x => x._rol)
                .ToList();
        }

        public Usuarios Insertar(Usuarios entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Roles!.Any(x => x.Id == entidad.Rol))
                throw new Exception("Rol invalido");

            if (this.conexion.Usuarios!.Any(x => x.Id != entidad.Id && x.Correo == entidad.Correo))
                throw new Exception("Correo repetido");

            if (entidad.Activo == null)
                entidad.Activo = false;

            this.conexion.Usuarios!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Usuarios entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                entidad.Rol == null || entidad.Rol == 0;
        }
    }
}
