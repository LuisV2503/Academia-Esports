using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EntretenimientosAplicacion : IEntretenimientosAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public EntretenimientosAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Entretenimientos Actualizar(Entretenimientos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Entretenimientos!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            var entry = this.conexion!.Entry<Entretenimientos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el entretenimiento {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Entretenimientos Borrar(Entretenimientos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Entretenimientos!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Entretenimientos!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el entretenimiento {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Entretenimientos> Buscar(Entretenimientos entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Entretenimientos!
                .Include(x => x._eventos)
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Entretenimientos> Consultar()
        {
            return this.conexion.Entretenimientos!
                .Include(x => x._eventos)
                .ToList();
        }

        public Entretenimientos Insertar(Entretenimientos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            this.conexion.Entretenimientos!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el entretenimiento {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Entretenimientos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Duracion == null || entidad.Duracion == 0 ||
                entidad.Evento == null || entidad.Evento == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
