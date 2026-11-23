using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public RolesAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Roles Actualizar(Roles entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Roles!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (this.conexion.Roles!.Any(x => x.Id != entidad.Id && x.Nombre == entidad.Nombre))
                throw new Exception("Rol repetido");

            var entry = this.conexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el rol {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Usuarios!.Any(x => x.Rol == entidad.Id))
                throw new Exception("Rol en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Roles!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Roles!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el rol {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Roles> Buscar(Roles entidad)
        {
            var nombre = entidad.Nombre ?? "";

            var lista = this.conexion.Roles!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();

            return lista;
        }

        public List<Roles> Consultar()
        {
            var lista = this.conexion.Roles!
                .ToList();

            return lista;
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

            this.auditoria.Registrar($"insertó el rol {entidad.Nombre} (Id {entidad.Id})");
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
