using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EventosAplicacion : IEventosAplicacion
    {
        private IConexion conexion;

        public EventosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Eventos Actualizar(Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaFin < entidad.FechaIni)
                throw new Exception("Fechas invalidas");

            if (entidad.Activo == null)
                entidad.Activo = false;

            var entry = this.conexion!.Entry<Eventos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
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

            this.conexion.Eventos!.Remove(entidad!);
            this.conexion.SaveChanges();
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
                string.IsNullOrEmpty(entidad.Direccion);
        }
    }
}
