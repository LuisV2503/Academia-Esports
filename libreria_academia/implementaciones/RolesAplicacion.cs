using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion conexion;

        public RolesAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Roles Actualizar(Roles entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (this.conexion.Roles!.Any(x => x.Id != entidad.Id && x.Nombre == entidad.Nombre))
                throw new Exception("Rol repetido");

            var entry = this.conexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Usuarios!.Any(x => x.Rol == entidad.Id))
                throw new Exception("Rol en uso");

            this.conexion.Roles!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Roles> Buscar(Roles entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Roles!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Roles> Consultar()
        {
            return this.conexion.Roles!
                .ToList();
        }

        public Roles Insertar(Roles entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (this.conexion.Roles!.Any(x => x.Id != entidad.Id && x.Nombre == entidad.Nombre))
                throw new Exception("Rol repetido");

            this.conexion.Roles!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Roles entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}
