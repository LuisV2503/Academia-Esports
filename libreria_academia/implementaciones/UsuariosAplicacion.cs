using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public UsuariosAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Usuarios!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

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

            this.auditoria.Registrar($"actualizó el usuario {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Logins!.Any(x => x.Usuario == entidad.Id) ||
                this.conexion.Auditorias!.Any(x => x.Usuario == entidad.Id))
                throw new Exception("Usuario en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Usuarios!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Usuarios!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el usuario {anterior.Nombre} (Id {anterior.Id})");
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

            this.auditoria.Registrar($"insertó el usuario {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Usuarios entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                entidad.Rol == null || entidad.Rol == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
