using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EventosAplicacion : IEventosAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public EventosAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Eventos Actualizar(Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Eventos!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaFin < entidad.FechaIni)
                throw new Exception("Fechas invalidas");

            if (entidad.Activo == null)
                entidad.Activo = false;

            var entry = this.conexion!.Entry<Eventos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el evento {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Eventos Borrar(Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Empresas_Academias_Eventos!.Any(x => x.Evento == entidad.Id) ||
                this.conexion.Eventos_Organizadores!.Any(x => x.Evento == entidad.Id) ||
                this.conexion.Entretenimientos!.Any(x => x.Evento == entidad.Id))
                throw new Exception("Evento en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Eventos!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Eventos!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el evento {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Eventos> Buscar(Eventos entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Eventos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Eventos> Consultar()
        {
            return this.conexion.Eventos!
                .ToList();
        }

        public Eventos Insertar(Eventos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaFin < entidad.FechaIni)
                throw new Exception("Fechas invalidas");

            if (entidad.Activo == null)
                entidad.Activo = false;

            this.conexion.Eventos!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el evento {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Eventos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.FechaIni == null ||
                entidad.FechaFin == null ||
                string.IsNullOrEmpty(entidad.Modalidad) ||
                string.IsNullOrEmpty(entidad.Ciudad) ||
                string.IsNullOrEmpty(entidad.Direccion) ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
